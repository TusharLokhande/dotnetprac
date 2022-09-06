import { TextField } from "@mui/material";
import { DatePicker, LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";
import { request } from "http";
import moment from "moment";
import React, { useState, useEffect } from "react";
import DynamicGrid from "../../../Components/DynamicGrid/DynamicGrid";
import SelectForm from "../../../Components/SelectForm/SelectForm";
import { APICall } from "../../../Helpers/API/APICalls";
import {
  getCapacityUtilizationCustomerData,
  getCapacityUtilizationEngagement,
  getCapacityUtilizationEngagementTypeOption,
  getCapacityAllocation,
} from "../../../Helpers/API/APIEndPoints";
import "./CapacityUtilizationReport.css";

const CapacityUtilizationReport = () => {
  const [fromDate, setFromDate] = useState<Date | null>(null);
  const [toDate, setToDate] = useState<Date | null>(null);
  const [customerOptions, setCustomerOptions] = useState([]);
  const [customerSelectData, setCustomerSelectData] = useState([]);
  const [engagementOptions, setEngagementOptions] = useState([]);
  const [engagementSelectedData, setEngagementSelectedData] = useState([]);
  const [engagementTypeOptions, setEngagementTypeOptions] = useState([]);
  const [engagementTypeSelectedData, setEngagementTypeSelectedData] = useState(
    []
  );
  const [masterType, setMasterType] = useState({
    value: "Engagement",
    label: "Engagement",
    id: 1,
  });
  const [requestObject, setRequestObject] = useState([]);
  const [gridType, setGridType] = useState([]);
  const [pageSize, setPageSize] = useState(10);
  const [start, setStart] = useState(0);
  const [sortColumn, setSortColumn] = useState("");
  const [sortDirection, setSortDirection] = useState("");
  const [searchText, setSearchText] = useState("");
  const [count, setCount] = useState(0);

  useEffect(() => {
    GetCustomerData();
    GetEngagementData();
    GetEngagementTypeData();
  }, []);

  useEffect(() => {
    let customers = ConvertArrayToString(customerSelectData);
    let engagements = ConvertArrayToString(engagementSelectedData);
    let engagementTypes = ConvertArrayToString(engagementTypeSelectedData);

    let requestData = {
      MasterType: masterType.value,
      Customers: customers,
      Engagements: engagements,
      EngagementTypes: engagementTypes,
      FromDate: moment(fromDate).format(moment.HTML5_FMT.DATE),
      ToDate: moment(toDate).format(moment.HTML5_FMT.DATE),
      sortColumn,
      sortDirection,
      searchText,
    };

    let s = Response(requestData, masterType);
    console.log(requestData, s);
  }, [sortDirection, sortColumn, searchText]);

  const masterTypeOptions = [
    { value: "Engagement", label: "Engagement", id: 1 },
    { value: "Resource", label: "Resource", id: 2 },
    { value: "Engagement Resource", label: "Engagement-Resource", id: 3 },
    { value: "Engagement Type", label: "Engagement-Type", id: 4 },
  ];

  const GetCustomerData = async () => {
    let { data } = await APICall(
      getCapacityUtilizationCustomerData,
      "POST",
      {}
    );

    let options = [];

    data.map((item, index) => {
      options.push({ value: item.name, label: item.name, id: item.id });
    });

    setCustomerOptions(options);
  };

  const GetEngagementData = async () => {
    let { data } = await APICall(getCapacityUtilizationEngagement, "POST", {});

    let options = [];
    data.map((item, index) => {
      options.push({ value: item.name, label: item.name, id: item.id });
    });
    setEngagementOptions(options);
  };

  const GetEngagementTypeData = async () => {
    let { data } = await APICall(
      getCapacityUtilizationEngagementTypeOption,
      "POST",
      {}
    );
    console.log("EngagementType: ", data);
    let options = [];
    data.map((item, index) => {
      options.push({ value: item.name, label: item.name, id: item.id });
    });
    setEngagementTypeOptions(options);
  };

  let s = "";
  const SelectOnChange = (event, apiField) => {
    if (apiField === "customer") {
      setCustomerSelectData(event);
    }

    if (apiField === "engagement") {
      setEngagementSelectedData(event);
    }

    if (apiField === "engagementType") {
      setEngagementTypeSelectedData(event);
    }

    if (apiField === "fromDate") {
      setFromDate(event);
    }

    if (apiField === "toDate") {
      setToDate(event);
    }

    if (apiField === "masterType") {
      setMasterType(event);
      console.log(event.value);
      let customers = ConvertArrayToString(customerSelectData);
      let engagements = ConvertArrayToString(engagementSelectedData);
      let engagementTypes = ConvertArrayToString(engagementTypeSelectedData);

      let requestData = {
        MasterType: "",
        Customers: customers,
        Engagements: engagements,
        EngagementTypes: engagementTypes,
        FromDate: moment(fromDate).format(moment.HTML5_FMT.DATE),
        ToDate: moment(toDate).format(moment.HTML5_FMT.DATE),
      };

      if (event.value === "Resource") {
        requestData.MasterType = "Resource";
        let s = Response(requestData, "Resource");
        setGridType(gridColumns_Resource);
      }

      if (event.value === "Engagement") {
        requestData.MasterType = "Engagement";
        let s = Response(requestData, "");
        setGridType(gridColumns_Engagement);
      }

      if (event.value === "Engagement Resource") {
        requestData.MasterType = "Engagement Resource";
        let s = Response(requestData, "");
        setGridType(gridColumns_EngagementResource);
      }

      if (event.value === "Engagement Type") {
        requestData.MasterType = "Engagement Type";
        let s = Response(requestData, "");
        setGridType(gridColumns_EngagementType);
      }
    }
  };

  const ConvertArrayToString = (arr) => {
    let s = [];
    arr.map((i) => s.push(i.id));
    return s.join(",");
  };

  const Response = async (obj, api) => {
    const { data } = await APICall(getCapacityAllocation, "POST", obj);
    setRequestObject(data);
  };

  const gridColumns_Resource = [
    {
      name: "resource",
      label: "Resource",
      options: {},
    },
    {
      name: "engagementType",
      label: "Engagement Type",
      options: {},
    },
    {
      name: "mandaysPlanned",
      label: "Mandays Planned",
      options: {},
    },
    {
      name: "mandaysActual",
      label: "Mandays Actual",
      options: {},
    },
    {
      name: "variance",
      label: "Variance",
      options: {},
    },
  ];

  const gridColumns_Engagement = [
    {
      name: "customer",
      label: "Customer",
    },
    {
      name: "engagement",
      label: "Engagement",
    },
    {
      name: "engagementType",
      label: "Engagement Type",
    },
    {
      name: "mandaysPlanned",
      label: "Mandays Planned",
    },
    {
      name: "mandaysActual",
      label: "Mandays Actual",
    },
    {
      name: "variance",
      label: "Variance",
    },
    {
      name: "nonCharged",
      label: "Non Charged",
    },
    {
      name: "poManDays",
      label: "PO Mandays",
    },
    {
      name: "budgetMandays",
      label: "Budget Mandays",
    },
    {
      name: "totalspendMandays",
      label: "Total Spend Mandays",
    },
    {
      name: "balanceMandays",
      label: "Balanced Mandays",
    },
    {
      name: "poValue",
      label: "PO Value",
    },
    {
      name: "poStatus",
      label: "PO Value",
    },
  ];

  const gridColumns_EngagementType = [
    {
      name: "engagementType",
      label: "Engagement Type",
    },
    {
      name: "mandaysPlanned",
      label: "Mandays Planned",
    },
    {
      name: "mandaysActual",
      label: "Mandays Actual",
    },
    {
      name: "variance",
      label: "Variance",
    },
    {
      name: "nonCharged",
      label: "Non Charged",
    },
  ];

  const gridColumns_EngagementResource = [
    {
      name: "customer",
      label: "Customer",
    },
    {
      name: "engagement",
      label: "Engagement",
    },
    {
      name: "engagementType",
      label: "Engagement Type",
    },
    {
      name: "resource",
      label: "Resource",
      options: {},
    },
    {
      name: "mandaysPlanned",
      label: "Mandays Planned",
    },
    {
      name: "mandaysActual",
      label: "Mandays Actual",
      options: {},
    },
    {
      name: "variance",
      label: "Variance",
    },
    {
      name: "poManDays",
      label: "PO Mandays",
    },
    {
      name: "budgetMandays",
      label: "Budget Mandays",
    },
    {
      name: "totalspendMandays",
      label: "Total Spend Mandays",
    },
    {
      name: "balanceMandays",
      label: "Balanced Mandays",
    },
  ];

  const objects = {
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
      setStart(page * pageSize);
    },
  };

  return (
    <>
      <div className="main">
        <section className="bg_breadcrumb">
          <div className="container-fluid">
            <div className="row">
              <div className="col-lg-12">
                <h4>Capacity Utizaltion Report</h4>
              </div>
            </div>
          </div>
        </section>

        <section className="main_content">
          <div className="container-fluid">
            <div className="row">
              <div className="col-lg-4 col-md-4 col-sm-6">
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

              <div className="col-lg-4 col-md-4 col-sm-6">
                <div className="form-group">
                  <label>Select Engagement</label>
                  <SelectForm
                    value={engagementSelectedData}
                    onChange={(e) => SelectOnChange(e, "engagement")}
                    options={engagementOptions}
                    isMulti
                    isSearchable={false}
                    isClearable={true}
                    closeMenuOnSelect={false}
                    hideSelectedOptions={false}
                    placeholder="Select Engagement"
                  />
                </div>
              </div>

              <div className="col-lg-4 col-md-4 col-sm-6">
                <div className="form-group">
                  <label>Select EngagementType</label>
                  <SelectForm
                    value={engagementTypeSelectedData}
                    onChange={(e) => SelectOnChange(e, "engagementType")}
                    options={engagementTypeOptions}
                    isMulti
                    isSearchable={false}
                    isClearable={true}
                    closeMenuOnSelect={false}
                    hideSelectedOptions={false}
                    placeholder="Select Engagement"
                  />
                </div>
              </div>
              <div className="col-lg-4 col-md-4 col-sm-6">
                <div className="form-group">
                  <LocalizationProvider dateAdapter={AdapterDateFns}>
                    <DatePicker
                      label="from Date"
                      value={fromDate}
                      onChange={(e) => SelectOnChange(e, "fromDate")}
                      inputFormat="dd/MM/yyyy"
                      renderInput={(params) => (
                        <TextField size="small" required {...params} />
                      )}
                    />
                  </LocalizationProvider>
                </div>
              </div>

              <div className="col-lg-4 col-md-4 col-sm-6">
                <div className="form-group">
                  <LocalizationProvider dateAdapter={AdapterDateFns}>
                    <DatePicker
                      label="To Date"
                      value={toDate}
                      onChange={(e) => SelectOnChange(e, "toDate")}
                      inputFormat="dd/MM/yyyy"
                      renderInput={(params) => (
                        <TextField size="small" required {...params} />
                      )}
                    />
                  </LocalizationProvider>
                </div>
              </div>

              <div className="col-lg-4 col-md-4 col-sm-6">
                <SelectForm
                  key={1}
                  options={masterTypeOptions}
                  placeholder="Select Master Type"
                  isDisabled={false}
                  value={masterType}
                  onChange={(e) => SelectOnChange(e, "masterType")}
                  isMulti={false}
                  noIndicator={false}
                  noSeparator={false}
                />
              </div>
            </div>
          </div>
        </section>

        <section className="main_content">
          <DynamicGrid
            options={objects}
            data={requestObject}
            columns={gridType}
          />
        </section>
      </div>
    </>
  );
};

export default CapacityUtilizationReport;
