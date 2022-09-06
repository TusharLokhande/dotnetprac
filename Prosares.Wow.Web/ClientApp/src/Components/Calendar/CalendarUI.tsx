import React, { useEffect, useState } from "react";
import Calendar from "react-calendar";
import "./CalendarUI.css";
import "react-calendar/dist/Calendar.css";
import {
  FaCheckCircle,
  FaHourglassHalf,
  FaTimesCircle,
  FaPowerOff,
  FaCalendarPlus,
  FaMinusCircle
} from "react-icons/fa";
import Heading from "../Heading/Heading";
import { APICall } from "../../Helpers/API/APICalls";
import {
  getCalendarData,
  GetDropDownList,
} from "../../Helpers/API/APIEndPoints";
import { getContext } from "../../Helpers/Context/Context";
import moment from "moment";
import { Card } from "react-bootstrap";
import SelectForm from "../SelectForm/SelectForm";

const CalendarUI = () => {
  const [value, onChange] = useState(new Date());
  const [showDesc, setShowDesc] = useState("");
  const [apiData, setApiData] = useState(null);
  let checkdate: any = moment().format(moment.HTML5_FMT.DATE);
  const [month, setMonth] = useState(Number(checkdate.split("-")[1]));
  const [year, setYear] = useState(Number(checkdate.split("-")[0]));
  const [resourceAllocatedOptions, setResourceAllocatedOptions] = useState<any>(
    []
  );
  const { EmployeeId, RoleId } = getContext();
  const RoleIdArray = RoleId.split(",");
  const [resourceAllocated, setResourceAllocated] = useState<any>({
    value: Number(EmployeeId),
  });
  const [formError, setFormError] = useState({});
  let formErrorObj = {};

  const selectOnChange = async (event: any, apiFieldName: any) => {
    if (apiFieldName === "resourceAllocated") {
      setResourceAllocated(event);
      if (event && Object.keys(event).length > 0) {
      } else if (!event || Object.keys(event).length === 0) {
        setFormError((preState) => ({
          ...preState,
          ["resourceAllocated_isEmpty"]: "Resource allocated can not be empty",
        }));
        formErrorObj["resourceAllocated_isEmpty"] =
          "Resource allocated can not be empty";
      }
    }
  };

  const getData = async () => {
    const obj = {
      EmployeeId: resourceAllocated.value,
      Month: Number(month),
      Year: Number(year),
    };
    const { data } = await APICall(getCalendarData, "POST", obj);
    await data.map(
      (ele) =>
        (ele.date = new Date(moment(ele.date).format(moment.HTML5_FMT.DATE)))
    );
    setApiData(data);
  };

  const getCalenderDataOnMonthChange = async (selectedMonth, selectedYear) => {
    const obj = {
      EmployeeId: Number(resourceAllocated.value),
      Month: Number(selectedMonth),
      Year: Number(selectedYear),
    };
    const { data } = await APICall(getCalendarData, "POST", obj);
    await data.map(
      (ele) =>
        (ele.date = new Date(moment(ele.date).format(moment.HTML5_FMT.DATE)))
    );
    setApiData(data);
  };

  useEffect(() => {
    resourceApiCall();
  }, []);

  useEffect(() => {
    getData();
  }, [resourceAllocated]);

  const tileContent = ({ activeStartDate, date, view }) => {
    let tempApiData = [...apiData];
    // if (date.toLocaleDateString() === new Date().toLocaleDateString()) {
    //   return (
    //     <p>
    //       <FaHourglassHalf color="orange" />
    //     </p>
    //   );
    // }
    if (
      view === "month" &&
      tempApiData.find((ele) => ele.date.getDate() === date.getDate())
    ) {
      let status = tempApiData.find(
        (ele) => ele.date.getDate() === date.getDate()
      ).status;
      switch (status) {
        case "full day approved leave":
          return (
            <p>
              <FaCalendarPlus color="blue" />
            </p>
          );
        case "first half day approved leave":
          return (
            <p>
              <FaCalendarPlus color="aqua" />
            </p>
          );
        case "second half day approved leave":
          return (
            <p>
              <FaCalendarPlus color="aqua" />
            </p>
          );
        // case "first half day approved leave and discrepancy":
        //   return (
        //     <p>
        //       <FaCalendarPlus color="blue" />{" "}<FaCheckCircle color="yellow" />
        //     </p>
        //   );
        // case "second half day approved leave and discrepancy":
        //   return (
        //     <p>
        //       <FaCalendarPlus color="blue" />{" "}<FaCheckCircle color="yellow" />
        //     </p>
        //   );
        case "applied leave":
          return (
            <p>
              <FaCalendarPlus color="orange" />
            </p>
          );
        case "holiday":
          return (
            <p>
              <FaPowerOff color="grey" />
            </p>
          );
        case "weekend":
          return (
            <p>
              <FaMinusCircle color="maroon" />
            </p>
          );
        case "notfilled":
          return (
            <p>
              <FaTimesCircle color="red" />
            </p>
          );
        case "full":
          return (
            <p>
              <FaCheckCircle color="green" />
            </p>
          );
        case "discrepancy":
          return (
            <p>
              <FaCheckCircle color="yellow" />
            </p>
          );
        case "today":
          return (
            <p>
              <FaHourglassHalf color="orange" />
            </p>
          );
        default:
          return null;
      }
    }
  };

  const onMonthChange = async ({ action, activeStartDate, value, view }) => {
    if (view === "month") {
      let currentMonthFirstDate: any = moment(new Date(activeStartDate)).format(
        moment.HTML5_FMT.DATE
      );
      let selectedMonth = Number(currentMonthFirstDate.split("-")[1]);
      let selectedYear = Number(currentMonthFirstDate.split("-")[0]);
      setMonth(Number(currentMonthFirstDate.split("-")[1]));
      setYear(Number(currentMonthFirstDate.split("-")[0]));
      await getCalenderDataOnMonthChange(selectedMonth, selectedYear);
    }
  };

  const resourceApiCall = async () => {
    let postObject = {
      searchFor: "employee",
      searchValue: 1,
    };
    const { data } = await APICall(GetDropDownList, "POST", postObject);
    if (data != undefined || data != null) {
      let employeeArray = [];
      await data.map((employee: any) => {
        let tempObj = {
          value: employee.id,
          label: employee.name,
        };
        if (tempObj.value == resourceAllocated.value) {
          setResourceAllocated(tempObj);
        }
        employeeArray.push(tempObj);
      });
      setResourceAllocatedOptions(employeeArray);
    }
  };

  //will use in future//
  const onDateChange = (date) => {
    let temp = { ...apiData };
    if (date.getMonth() === temp.month) {
      let ele = temp.days.find((ele) => ele.date.getDate() === date.getDate());
      if (ele !== undefined) {
        setShowDesc(
          `Status is ${ele.status} & description is ${ele.description}`
        );
      } else {
        setShowDesc("");
      }
      onChange(date);
    }
  };
  //will use in future//

  return (
    <>
      {apiData && (
        <>
          <Heading title={"Calendar"} />
          <section className="main_content">
            <div className="container-fluid">
              <div className="row">
                <div className="col-lg-3 col-md-4 col-sm-6">
                  <div className="form-group">
                    <label>
                      Employee Selected<sup>*</sup>
                    </label>
                    <SelectForm
                      options={resourceAllocatedOptions}
                      placeholder="Select"
                      isDisabled={
                        RoleIdArray.includes("3") ||
                          RoleIdArray.includes("4") ||
                          RoleIdArray.includes("6")
                          ? false
                          : true
                      }
                      value={resourceAllocated}
                      onChange={(event) =>
                        selectOnChange(event, "resourceAllocated")
                      }
                      isMulti={false}
                      noIndicator={false}
                      noSeparator={false}
                    />
                    <span style={{ color: "red" }}>
                      {formError["resourceAllocated_isEmpty"]}
                    </span>
                  </div>
                </div>
                <div className="row d-flex flex-row gap-1 justify-content-start align-items-center">
                  <div className="d-inline col-lg-4 col-md-6 col-sm-6">
                    <Calendar
                      onChange={() => { }} //onDateChange
                      value={value}
                      tileContent={tileContent}
                      showNeighboringMonth={false}
                      onActiveStartDateChange={onMonthChange}
                      minDetail="century"
                      minDate={new Date(new Date().getFullYear(), 0, 1)}
                      maxDate={new Date(new Date().getFullYear() + 1, 0, 31)}
                    />
                  </div>
                  <div className="d-inline col-lg-1 col-md-1 col-sm-6">
                    <Card style={{ width: "28rem" }}>
                      <Card.Body>
                        <Card.Title>Calendar Legends</Card.Title>
                        <Card.Text>
                          <p>
                            Today : <FaHourglassHalf color="orange" />
                          </p>
                          <p>
                            Full day : <FaCheckCircle color="green" />
                          </p>
                          <p>
                            Discrepancy : <FaCheckCircle color="yellow" />
                          </p>
                          <p>
                            Full Day Approved Leave : <FaCalendarPlus color="blue" />
                          </p>
                          <p>
                            Half Day Approved Leave : <FaCalendarPlus color="aqua" />
                          </p>
                          {/* <p>
                            Second Half Day Approved Leave : <FaBusinessTime />
                          </p> */}
                          {/* <p>
                            Half day approved leave and discrepancy : <FaCalendarPlus color="blue" />{" "}<FaCheckCircle color="yellow" />
                          </p> */}
                          {/* <p>
                            Second half day approved leave and discrepancy : <FaBusinessTime />{" "}<FaCheckCircle color="yellow" />
                          </p> */}
                          <p>
                            Applied Leave : <FaCalendarPlus color="orange" />
                          </p>
                          <p>
                            Holiday : <FaPowerOff color="grey" />
                          </p>
                          <p>
                            Not filled : <FaTimesCircle color="red" />
                          </p>
                          <p>
                            Weekend : <FaMinusCircle color="maroon" />
                          </p>
                        </Card.Text>
                      </Card.Body>
                    </Card>
                  </div>
                </div>
              </div>
            </div>
          </section>
        </>
      )}
      {/* <h4 className="text-center mb-1">{value.toDateString()}</h4>
      {showDesc && <h4 className="text-center">{showDesc.toString()}</h4>} */}
    </>
  );
};

export default CalendarUI;
