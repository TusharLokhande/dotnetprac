import { APICall } from "../../../Helpers/API/APICalls";
import React, { useState, useEffect } from "react";
import { TimeSheetPolicyApi } from "../../../Helpers/API/APIEndPoints";
import DynamicGrid from "../../../Components/DynamicGrid/DynamicGrid";
import { Link, useNavigate } from "react-router-dom";
import Heading from "../../../Components/Heading/Heading";

const TimeSheetPolicy = () => {
  const [data, setData] = useState([]);

  const call = async () => {
    const { data } = await APICall(TimeSheetPolicyApi, "POST", {});
    console.log(data);
    setData(data);
  };

  useEffect(() => {
    call();
  }, []);

  let navigate = useNavigate();

  const gridColumns = [
    {
      name: "name",
      label: "name",
      options: {},
    },
    {
      name: "isTimeSheetApplicable",
      label: "isTimeSheetApplicable",
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
      label: "isActive",
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
          selectableRows: "none",
          serverSide: true,
          rowsPerPageOptions: [],
          download: false,
          print: false,
          viewColumns: false,
          filter: false,
          search: true,
        }}
        columns={gridColumns}
      />
    </>
  );
};

export default TimeSheetPolicy;
