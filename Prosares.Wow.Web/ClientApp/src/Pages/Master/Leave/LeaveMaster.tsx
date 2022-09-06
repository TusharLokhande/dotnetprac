import React, { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import DynamicGrid from "../../../Components/DynamicGrid/DynamicGrid";
import Heading from "../../../Components/Heading/Heading";
import { APICall } from "../../../Helpers/API/APICalls";
import {
  getLeaveRequestMasterGridData,
  leaveExportToExcel,
} from "../../../Helpers/API/APIEndPoints";
import moment from "moment";
import { getContext, LoaderContext } from "../../../Helpers/Context/Context";
import axios from "axios";
// Import to download excel file
import fileDownload from "js-file-download";
import { Tooltip } from "@mui/material";

const LeaveRequestMaster = () => {
  let navigate = useNavigate();
  const { showLoader, hideLoader } = useContext(LoaderContext);
  const [LeaveRequestMasterData, setLeaveRequestMasterData] = useState([]);
  const [pageSize, setPageSize] = useState(10);
  const [start, setStart] = useState(0);
  const [sortColumn, setSortColumn] = useState("");
  const [sortDirection, setSortDirection] = useState("");
  const [searchText, setSearchText] = useState("");
  const [count, setCount] = useState(0);
  const [roleForData, setRoleForData] = useState(0);
  const { EmployeeId, RoleId } = getContext();
  const RoleIdArray = RoleId.split(",");

  useEffect(() => {
    if (
      RoleIdArray.includes("3") ||
      RoleIdArray.includes("4") ||
      RoleIdArray.includes("6")
    ) {
      setRoleForData(1); // 1 to get all request
    } else if (RoleIdArray.includes("2")) {
      setRoleForData(2); // 2 to get all request only for approver
    } else if (RoleIdArray.includes("1")) {
      setRoleForData(3); // 3 to get all request only for requestor
    }
  }, [RoleIdArray]);

  const workday_count = (start, end) => {
    var first = start.clone().endOf("week"); // end of first week
    var last = end.clone().startOf("week"); // start of last week
    var days = (last.diff(first, "days") * 5) / 7; // this will always multiply of 7
    var wfirst = first.day() - start.day(); // check first week
    if (start.day() == 0) --wfirst; // -1 if start with sunday
    var wlast = end.day() - last.day(); // check last week
    if (end.day() == 6) --wlast; // -1 if end with saturday
    return wfirst + Math.floor(days) + wlast; // get the total
  };

  const getLeaveRequestMasterData = async () => {
    showLoader();
    let requestParams = {
      start: start,
      pageSize: pageSize,
      sortColumn: sortColumn,
      sortDirection: sortDirection,
      searchText: searchText,
      RoleId: roleForData,
      EmployeeId: Number(EmployeeId),
    };
    const { data } = await APICall(
      getLeaveRequestMasterGridData,
      "POST",
      requestParams
    );

    await data.leaveRequestsMasterData.forEach((ele: any) => {
      ele.fromDate = moment(ele.fromDate).format("DD-MM-yyyy");
      ele.toDate = moment(ele.toDate).format("DD-MM-yyyy");
      // let leaveDays: any;
      // leaveDays = workday_count(FromDate, ToDate);
      // ele.leaveDays = leaveDays;
    });
    setCount(data.count);
    setLeaveRequestMasterData(data.leaveRequestsMasterData);
    hideLoader();
  };

  const ExportToExcel = () => {
    let requestParams = {
      start: 0,
      pageSize: count,
      sortColumn: sortColumn,
      sortDirection: sortDirection,
      searchText: searchText,
      RoleId: roleForData,
      EmployeeId: Number(EmployeeId),
    };

    axios
      .request({
        responseType: "blob",
        method: "POST",
        url: leaveExportToExcel,
        data: requestParams,
        headers: {
          "Access-Control-Allow-Origin": "*",
          "Content-Type": "application/json",
          "Access-Control-Allow-Headers": "*",
        },
      })
      .then((response) => {
        fileDownload(
          response.data,
          `Leaves-` +
            `${moment(new Date()).format(moment.HTML5_FMT.DATE)}` +
            `.xlsx`
        );
      })
      .catch((error) => {
        console.log(error);
      });
  };

  useEffect(() => {
    if (roleForData !== 0) {
      getLeaveRequestMasterData();
    }
  }, [start, sortColumn, sortDirection, searchText, count, roleForData]); // start, sortColumn, sortDirection, searchText, count

  const gridColumns = [
    {
      name: "id",
      label: "Id",
      options: {
        display: false,
      },
    },
    {
      name: "approverName",
      label: "Approver",
      options: {
        filter: false,
        sort: false,
        sortDescFirst: true,
      },
    },
    {
      name: "requesterName",
      label: "Requestor",
      options: {
        filter: false,
        sort: false,
        sortDescFirst: true,
      },
    },
    // {
    //   name: "leavesReson",
    //   label: "Reason",
    //   options: {
    //     filter: false,
    //     sort: true,
    //     sortDescFirst: true,
    //   },
    // },
    {
      name: "fromDate",
      label: "From Date",
      options: {
        filter: false,
        sort: false,
        sortDescFirst: true,
      },
    },
    {
      name: "toDate",
      label: "To Date",
      options: {
        filter: false,
        sort: false,
        sortDescFirst: true,
      },
    },
    {
      name: "leaveDays",
      label: "Leave Days",
      options: {
        filter: false,
        sort: false,
        sortDescFirst: true,
      },
    },
    {
      name: "requestStatus",
      label: "Status",
      options: {
        filter: false,
        sort: false,
        sortDescFirst: true,
        customBodyRender: (value, tableMeta) => {
          if (value === 1) {
            return "Approved";
          } else if (value === 2) {
            return "Rejected";
          } else if (value === 3) {
            return "Pending";
          } else if (value === 4) {
            return "Cancelled";
          }
        },
      },
    },
    // {
    //   name: "remark",
    //   label: "Remark",
    //   options: {
    //     filter: false,
    //     sort: true,
    //     sortDescFirst: true,
    //   },
    // },
    // {
    //   name: "isActive",
    //   label: "Active",
    //   options: {
    //     filter: false,
    //     sort: false,
    //     customBodyRender: (value, tableMeta) => {
    //       if (value) {
    //         return <i className="fas fa-check"></i>;
    //       } else {
    //         return <i className="fas fa-times"></i>;
    //       }
    //     },
    //   },
    // },
    {
      name: "",
      label: "Action",
      options: {
        filter: false,
        sort: false,
        customBodyRender: (value, tableMeta) => {
          // console.log(tableMeta.tableData[tableMeta.rowIndex]);
          let approverId = tableMeta.tableData[tableMeta.rowIndex].approverId;
          let requestorId = tableMeta.tableData[tableMeta.rowIndex].requestorId;
          let requestStatus =
            tableMeta.tableData[tableMeta.rowIndex].requestStatus;

          let id = tableMeta.tableData[tableMeta.rowIndex].id;

          if (requestorId === Number(EmployeeId) && requestStatus === 3) {
            return (
              <>
                <a
                  style={{ cursor: "pointer" }}
                  onClick={(e) => {
                    e.preventDefault();
                    navigate("edit", { state: { id, role: "self" } });
                  }}>
                  <i className="fa fa-pencil" aria-hidden="true"></i>
                </a>
              </>
            );
          } else if (approverId === Number(EmployeeId) && requestStatus === 3) {
            return (
              <>
                <a
                  style={{ cursor: "pointer" }}
                  onClick={(e) => {
                    e.preventDefault();
                    navigate("edit", { state: { id, role: "approver" } });
                  }}>
                  <i className="fa fa-pencil" aria-hidden="true"></i>
                </a>
              </>
            );
          } else {
            return (
              <>
                <a
                  style={{ cursor: "pointer" }}
                  onClick={(e) => {
                    e.preventDefault();
                    navigate("edit", { state: { id, role: "viewer" } });
                  }}>
                  <i
                    className="fa fa-eye text-secondary"
                    aria-hidden="true"></i>
                </a>
              </>
            );
          }
        },
      },
    },
  ];

  const options = {
    selectableRows: "none",
    count: count,
    rowsPerPage: pageSize,
    serverSide: true,
    rowsPerPageOptions: [],
    download: false,
    print: false,
    viewColumns: false,
    filter: false,
    search: true,
    onChangeRowsPerPage: (num) => {
      //   setLimit(num);
      //   setNxtPgInfo("");
      //   setPrevPgInfo("");
      //   setIsPrevOrNext("");
    },
    onSearchChange: (searchText) => {
      if (searchText !== null) {
        setSearchText(searchText);
      } else {
        setSearchText("");
      }
    },
    onColumnSortChange: async (sortColumn, sortDirection) => {
      if (sortDirection === "asc") {
        await setSortColumn(sortColumn);
        await setSortDirection(sortDirection);
      }
      if (sortDirection === "desc") {
        await setSortColumn(sortColumn);
        await setSortDirection(sortDirection);
      }
    },
    onChangePage: async (page) => {
      setStart(page * pageSize);
    },
  };

  return (
    <>
      <Heading title={"Request Leave /Report Absence"} />
      <div className="px-3 pt-3 d-flex justify-content-end">
        <Tooltip title="Export to excel">
          <button
            onClick={() => ExportToExcel()}
            className="btn btn-secondary mr-2">
            <i className="fa-solid fa-file-arrow-down"></i>
          </button>
        </Tooltip>
        <button onClick={() => navigate("edit")} className="btn btn-primary">
          + Create
        </button>
      </div>
      <DynamicGrid
        options={options}
        data={LeaveRequestMasterData}
        columns={gridColumns}
      />
    </>
  );
};
export default LeaveRequestMaster;
