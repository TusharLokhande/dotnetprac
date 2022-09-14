import { TextField, Tooltip } from "@mui/material";
import { DatePicker, LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";
import { sub } from "date-fns";
import React, { useContext, useEffect, useState } from "react";

import { useNavigate } from "react-router-dom";
import DynamicGrid from "../../../Components/DynamicGrid/DynamicGrid";
import Heading from "../../../Components/Heading/Heading";
import SelectForm from "../../../Components/SelectForm/SelectForm";
import { APICall } from "../../../Helpers/API/APICalls";
import {
  GetDropDownList,
  getCapacityUtilizationEngagementTypeOption,
  GetMileStoneReportData,
  MileStoneReportExportToExcel,
} from "../../../Helpers/API/APIEndPoints";
import { LoaderContext } from "../../../Helpers/Context/Context";
import moment from "moment";
import axios from "axios";
import fileDownload from "js-file-download";

const MileStoneReport = () => {
  const [fromDate, setFromDate] = useState<Date | null>(null);
  const [toDate, setToDate] = useState<Date | null>(null);
  const [customerOptions, setCustomerOptions] = useState([]);
  const [customerSelectData, setCustomerSelectData] = useState([]);
  const [engagementTypeOptions, setEngagementTypeOptions] = useState([]);
  const [engagementTypeSelectedData, setEngagementTypeSelectedData] = useState(
    []
  );
  const [requestObject, setRequestObject] = useState([{ count: 0 }]);
  const [pageSize, setPageSize] = useState(10);
  const [start, setStart] = useState(0);
  const [sortColumn, setSortColumn] = useState("");
  const [sortDirection, setSortDirection] = useState("");
  const [searchText, setSearchText] = useState("");
  const [count, setCount] = useState(0);
  const [formErrors, setFormErrors] = useState({});
  const { showLoader, hideLoader } = useContext(LoaderContext);

  let navigate = useNavigate();
  let formErrorObj = {};

  useEffect(() => {
    onLoadApiCall();
  }, []);

  useEffect(() => {
    let customers = ConvertArrayToString(customerSelectData);
    let engagementTypes = ConvertArrayToString(engagementTypeSelectedData);

    if (customers.length <= 0) {
      customers = "";
    }
    if (engagementTypes.length <= 0) {
      engagementTypes = "";
    }

    let requestData = {
      Customers: customers,
      EngagementTypes: engagementTypes,
      FromDate: moment(fromDate).format(moment.HTML5_FMT.DATE),
      ToDate: moment(toDate).format(moment.HTML5_FMT.DATE),
      sortColumn,
      sortDirection,
      searchText,
      pageSize,
      start,
      count,
    };

    getReportData(requestData);
  }, [sortDirection, sortColumn, searchText, start, pageSize]);

  const SelectOnChange = (event, apiField) => {
    if (apiField === "fromDate") {
      setFromDate(event);

      if (event) {
        setFormErrors((preState) => ({
          ...preState,
          ["fromDate_isEmpty"]: undefined,
        }));
      } else {
        setFormErrors((preState) => ({
          ...preState,
          ["fromDate_isEmpty"]: "From date can not be empty",
        }));
      }
    }
    if (apiField === "toDate") {
      setToDate(event);
      if (event) {
        setFormErrors((preState) => ({
          ...preState,
          ["toDate_isEmpty"]: undefined,
        }));
      } else {
        setFormErrors((preState) => ({
          ...preState,
          ["toDate_isEmpty"]: " To date can not be empty",
        }));
      }
    }

    if (apiField === "engagementType") {
      setEngagementTypeSelectedData(event);
    }

    if (apiField === "customer") {
      setCustomerSelectData(event);
    }
  };

  const submitFunc = async (apiField) => {
    if (apiField === "submit") {
      let customers = ConvertArrayToString(customerSelectData);
      let engagementTypes = ConvertArrayToString(engagementTypeSelectedData);

      let obj = {
        Customer: customers,
        EngagementType: engagementTypes,
        FromDate: moment(fromDate).format(moment.HTML5_FMT.DATE),
        ToDate: moment(toDate).format(moment.HTML5_FMT.DATE),
        sortColumn,
        sortDirection,
        searchText,
        pageSize,
        start,
        count,
      };

      const check = Validation();
      if (check) {
        await getReportData(obj);
      }
    }

    if (apiField === "reset") {
      setFromDate(null);
      setToDate(null);
      setFormErrors({});
      setCustomerSelectData([]);
      setEngagementTypeSelectedData([]);
    }
  };

  const ConvertArrayToString = (arr) => {
    let s = [];
    arr.map((i) => s.push(i.id));
    return s.join(",");
  };

  const ExportToExcel = async () => {
    let customers = ConvertArrayToString(customerSelectData);
    let engagementTypes = ConvertArrayToString(engagementTypeSelectedData);

    let obj = {
      Customer: customers,
      EngagementType: engagementTypes,
      FromDate: moment(fromDate).format(moment.HTML5_FMT.DATE),
      ToDate: moment(toDate).format(moment.HTML5_FMT.DATE),
      sortColumn,
      sortDirection,
      searchText,
      pageSize: count,
      start: 0,
      count,
    };

    const check = Validation();
    if (check) {
      axios
        .request({
          responseType: "blob",
          method: "POST",

          //data: Response,
          url: MileStoneReportExportToExcel,
          //data:JSON,
          headers: {
            "Access-Control-Allow-Origin": "*",
            "Content-Type": "application/json",
            "Access-Control-Allow-Headers": "*",
          },
          data: obj,
        })
        .then((response) => {
          fileDownload(response.data, "MileStone Report.xlsx");
        })
        .catch((error) => {
          console.log(error.response.data);
        });
    }
  };

  const onLoadApiCall = async () => {
    await customerApiCall();
    await GetEngagementTypeData();
    await getReportData({
      Customer: "",
      EngagementType: "",
      pageSize: 10,
    });
  };

  const getReportData = async (obj) => {
    showLoader();
    let x = { count: 0, data: [] };
    const { data } = await APICall(GetMileStoneReportData, "Post", obj);

    x = { ...data };
    setRequestObject(x.data);
    setCount(x.count);
    hideLoader();
    return;
  };

  const customerApiCall = async () => {
    let postObject = {
      searchFor: "customer",
      searchValue: 1,
    };
    const { data } = await APICall(GetDropDownList, "POST", postObject);
    if (data != undefined || data != null) {
      let customerArray = [];
      await data.map((customer: any) => {
        let tempObj = {
          abb: customer.abbreviation,
          value: customer.id,
          label: customer.name,
        };
        customerArray.push(tempObj);
      });
      setCustomerOptions(customerArray);
    }
  };

  const GetEngagementTypeData = async () => {
    let { data } = await APICall(
      getCapacityUtilizationEngagementTypeOption,
      "POST",
      {}
    );
    let options = [];
    data.map((item, index) => {
      options.push({ value: item.name, label: item.name, id: item.id });
    });
    options.sort((a, b) => a.value.localeCompare(b.value));
    setEngagementTypeOptions(options);
  };

  const Validation = () => {
    let objError = {};

    if (fromDate == undefined || fromDate == null) {
      objError["fromDate_isEmpty"] = "From date can not be empty";
    }

    if (toDate == undefined || toDate == null) {
      objError["toDate_isEmpty"] = "Valid Till date can not be empty";
    }

    formErrorObj = objError;
    setFormErrors(objError);
    const isEmpty = Object.keys(objError).length === 0;

    return isEmpty;
  };

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

  const gridColumns = [
    {
      name: "engagement",
      label: "Engagement",
      options: {},
    },
    {
      name: "engagementType",
      label: "Engagement Type",
      options: {},
    },

    {
      name: "mileStone",
      label: "Milestone",
      options: {},
    },
    {
      name: "poValue",
      label: "PO Value",
      options: {},
    },
    {
      name: "amount",
      label: "Amount",
      options: {},
    },
    {
      name: "plannedDate",
      label: "Planned Date",
      options: {
        customBodyRender: (value, tableMeta, updateValue) => {
          let date = moment(value).format(moment.HTML5_FMT.DATE);
          return (
            <>
              <p>{date}</p>
            </>
          );
        },
      },
    },
    {
      name: "revisedDate",
      label: "Actual Date",
      options: {
        customBodyRender: (value, tableMeta, updateValue) => {
          let date = moment(value).format(moment.HTML5_FMT.DATE);
          return (
            <>
              <p>{date}</p>
            </>
          );
        },
      },
    },
    {
      name: "invoicedDate",
      label: "Invoice Data",
      options: {
        customBodyRender: (value, tableMeta, updateValue) => {
          let date = moment(value).format(moment.HTML5_FMT.DATE);
          return (
            <>
              <p>{date}</p>
            </>
          );
        },
      },
    },
  ];
  return (
    <>
      <Heading title={"MileStone Report"} />

      <section className="main_content">
        <div className="container-fluid">
          <div className="row">
            <div className="col-lg-2 col-md-4 col-sm-6">
              <div className="form-group">
                <label>Select Customer</label>
                <SelectForm
                  value={customerSelectData}
                  onChange={(e) => SelectOnChange(e, "customer")}
                  options={customerOptions}
                  isMulti
                  isSearchable={false}
                  isClearable={true}
                  closeMenuOnSelect={false}
                  hideSelectedOptions={false}
                  placeholder="Select Customer"
                />
              </div>
            </div>

            <div className="col-lg-2 col-md-4 col-sm-6">
              <div className="form-group">
                <label>Select Engagement Type</label>
                <SelectForm
                  value={engagementTypeSelectedData}
                  onChange={(e) => SelectOnChange(e, "engagementType")}
                  options={engagementTypeOptions}
                  isMulti
                  isSearchable={false}
                  isClearable={true}
                  closeMenuOnSelect={false}
                  hideSelectedOptions={false}
                  placeholder="Select Engagement Type"
                />
              </div>
            </div>

            <div className="col-lg-2 col-md-4 col-sm-6 align-self-center">
              <div className="form-group">
                <label className="d-block">
                  From Date <sup>*</sup>
                </label>
                <LocalizationProvider dateAdapter={AdapterDateFns}>
                  <DatePicker
                    label="From Date"
                    value={fromDate}
                    onChange={(e) => SelectOnChange(e, "fromDate")}
                    inputFormat="dd/MM/yyyy"
                    renderInput={(params) => (
                      <TextField size="small" {...params} />
                    )}
                  />
                </LocalizationProvider>
              </div>
              <p style={{ color: "red" }}>{formErrors["fromDate_isEmpty"]}</p>
            </div>

            <div className="col-lg-2 col-md-4 col-sm-6 align-self-center">
              <div className="form-group">
                <label className="d-block">
                  To Date <sup>*</sup>
                </label>
                <LocalizationProvider dateAdapter={AdapterDateFns}>
                  <DatePicker
                    label="To Date"
                    value={toDate}
                    onChange={(e) => SelectOnChange(e, "toDate")}
                    inputFormat="dd/MM/yyyy"
                    minDate={fromDate}
                    renderInput={(params) => (
                      <TextField size="small" {...params} />
                    )}
                  />
                </LocalizationProvider>
              </div>
              <p style={{ color: "red" }}>{formErrors["toDate_isEmpty"]}</p>
            </div>

            {/* Buttons */}
            <div className="col-lg-3 col-md-4 col-sm-6 mt-4">
              <div className="d-flex justify-content-end gap-2">
                <Tooltip title="Export to excel">
                  <button
                    onClick={() => ExportToExcel()}
                    className="btn btn-secondary mr-2"
                  >
                    <i className="fa-solid fa-file-arrow-down"></i>
                  </button>
                </Tooltip>
                <button
                  onClick={() => submitFunc("reset")}
                  className="btn btn-reset ml-1"
                  // disabled={state !== null ? true : false}
                >
                  Reset
                </button>
                <button
                  style={{ background: "#96c61c" }}
                  onClick={() => submitFunc("submit")}
                  className="btn btn-save ml-1"
                >
                  Submit
                </button>
                <button
                  onClick={() => navigate("/")}
                  className="btn btn-secondary"
                >
                  Back
                </button>
              </div>
            </div>
          </div>
        </div>
      </section>
      <DynamicGrid
        options={options}
        data={requestObject}
        columns={gridColumns}
      />
    </>
  );
};

export default MileStoneReport;
