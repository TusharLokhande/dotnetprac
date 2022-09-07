import { APICall } from "../../../Helpers/API/APICalls";
import React, { useState, useEffect } from "react";
import { TimeSheetPolicyApi } from "../../../Helpers/API/APIEndPoints";
import DynamicGrid from "../../../Components/DynamicGrid/DynamicGrid";
import { Link, useNavigate } from "react-router-dom";
import Heading from "../../../Components/Heading/Heading";

const TimeSheetPolicy = () => {
  const [data, setData] = useState([]);
  const [pageSize, setPageSize] = useState(10);
  const [start, setStart] = useState(0);
  const [sortColumn, setSortColumn] = useState("");
  const [sortDirection, setSortDirection] = useState("");
  const [searchText, setSearchText] = useState("");
  const [count, setCount] = useState(0);

  const call = async () => {
    let obj = {
      start,
      sortColumn,
      sortDirection,
      searchText,
      count,
      pageSize,
    };
    let x = { count: 0, data: [] };
    const { data } = await APICall(TimeSheetPolicyApi, "POST", obj);
    x = { ...data };
    console.log(data);
    setData(x.data);
    setCount(x.count);
  };

  useEffect(() => {
    call();
  }, [searchText, sortColumn, sortDirection, start]);

  let navigate = useNavigate();

  const gridColumns = [
    {
      name: "name",
      label: "Name",
      options: {},
    },
    {
      name: "isTimeSheetApplicable",
      label: "Is TimeSheet Applicable",
      options: {
        filter: false,
        sort: false,
        customBodyRender: (value, tableMeta) => {
          if (value) {
            return (
              <i style={{ marginLeft: "100px" }} className="fas fa-check"></i>
            );
          } else {
            return (
              <i style={{ marginLeft: "100px" }} className="fas fa-times"></i>
            );
          }
        },
      },
    },
    {
      name: "isActive",
      label: "IsActive",
      options: {
        filter: false,
        sort: false,
        customBodyRender: (value, tableMeta) => {
          if (value) {
            return <i className="fas fa-check"></i>;
          } else {
            return <i className="fas fa-times"></i>;
          }
        },
      },
    },
    {
      name: "",
      label: "Action",
      options: {
        filter: false,
        sort: false,
        customBodyRender: (value, tableMeta) => {
          let id = tableMeta.tableData[tableMeta.rowIndex].id;

          return (
            <>
              <Link to={"edit"} state={id}>
                <i
                  className="fa fa-pencil text-secondary"
                  aria-hidden="true"
                ></i>
              </Link>
            </>
          );
        },
      },
    },
  ];

  return (
    <>
      <Heading title={"Timesheet Policy Master"} />
      <div className="px-3 pt-3 d-flex justify-content-end">
        <button onClick={() => navigate("edit")} className="btn btn-primary">
          + Create
        </button>
      </div>

      <DynamicGrid
        data={data}
        options={{
          // selectableRows: "none",
          // serverSide: true,
          // rowsPerPageOptions: [],
          // download: false,
          // print: false,
          // viewColumns: false,
          // filter: false,
          // search: true,

          selectableRows: "none",
          count: count,
          //rowsPerPage: pageSize,
          serverSide: true,
          rowsPerPageOptions: [],
          download: false,
          print: false,
          viewColumns: false,
          filter: false,
          search: true,
          onSearchChange: (searchText) => {
            if (searchText !== null) {
              setSearchText(searchText);
            } else {
              setSearchText("");
            }
          },
          onColumnSortChange: async (sortColumn, sortDirection) => {
            if (sortDirection === "asc") {
              console.log(sortColumn, sortDirection);
              await setSortColumn(sortColumn);
              await setSortDirection(sortDirection);
            }
            if (sortDirection === "desc") {
              await setSortColumn(sortColumn);
              await setSortDirection(sortDirection);
            }
            console.log(sortDirection, sortColumn);
          },
          onChangePage: async (page) => {
            console.log(page);
            setStart(page * pageSize);
          },
        }}
        columns={gridColumns}
      />
    </>
  );
};

export default TimeSheetPolicy;
