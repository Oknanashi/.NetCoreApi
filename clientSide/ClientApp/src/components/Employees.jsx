import React, { useEffect, useState, Fragment, useContext } from "react";
import { Select } from "semantic-ui-react";

import { withRouter } from "react-router-dom";
import axios from "axios";
import { makeStyles } from "@material-ui/core/styles";
import Modal from "@material-ui/core/Modal";
import Backdrop from "@material-ui/core/Backdrop";
import Fade from "@material-ui/core/Fade";
import UserContext from "./../UserContext";

const useStyles = makeStyles(theme => ({
  modal: {
    display: "flex",
    alignItems: "center",
    justifyContent: "center"
  },
  paper: {
    backgroundColor: theme.palette.background.paper,
    border: "2px solid #000",
    boxShadow: theme.shadows[5],
    padding: theme.spacing(2, 4, 3)
  }
}));
// const WrappingComponent = styled.div`
//   p {
//     overflow-wrap: break-word;
//     word-wrap: break-word;
//     hyphens: auto;
//   }
// `;

//Hardcoded but can be easily fetched from the database
const roleOptions = [
  { key: "Admin", value: "Admin", text: "Admin" },
  { key: "Member", value: "Member", text: "Member" },
  { key: "Manager", value: "Manager", text: "Manager" }
];

const Employees = () => {
  const { setConfig,notifySuccess} = useContext(UserContext);
  // const userStore = useContext(UserContext);
  const [employees, setEmployees] = useState();
  // const [employeeRole,setRole]=useState();
  const [open, setOpen] = useState(false);
  const [selectedEmployee, setSelectedEmployee] = useState(false);
  const classes = useStyles();

  const handleOpen = selectedEmployee => {
    console.log(selectedEmployee);
    setSelectedEmployee(selectedEmployee);
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
  };

  useEffect(() => {
    axios
      .get("https://localhost:5001/api/user/getemployees", setConfig)
      .then(data => {
        setEmployees(data.data);
        console.log(data);
      });
  }, []);

  const editRole = (role, employee) => {
    var userValue = {
      role: role,
      email: employee.email
    };
    axios
      .post(
        "https://localhost:5001/api/user/editEmployeeRole",
        userValue,
        setConfig
      )
      .then(response => {
        console.log(response);
      })
      .catch(e => {
        console.log(e);
      });
  };
  const removeEmployee = employeeToDelete => {
    console.log(employeeToDelete.id);
    axios
      .delete(
        `https://localhost:5001/api/user/${employeeToDelete.id}`,
        employeeToDelete.id,
        setConfig
      )
      .then(response => {
        notifySuccess("Employee was deleted")
        axios
          .get("https://localhost:5001/api/user/getemployees", setConfig)
          .then(data => {
            setEmployees(data.data);
            console.log(data);
          });
        handleClose();
      })
      .catch(e => {
        console.log(e);
      });
  };

  
  return (
    <Fragment>
      <div
        style={{
          display: "flex",
          WebkitJustifyContent: "space-around",
          paddingRight: "50%",
          alignItems: "baseline"
        }}
        className="item"
      >
        <h1 style={{ margin: 0 }}>Employees</h1>

        <h1 style={{ margin: 0 }}>Roles</h1>
      </div>
      <div className="ui celled list">
        <Fragment>
          {employees
            ? employees.map(employee => (
                <div
                  className="item"
                  style={{
                    display: "flex",
                    WebkitJustifyContent: "space-around"
                  }}
                  key={employee.id}
                >
                  <div className="content">
                    <div className="header">{employee.userName}</div>
                    {employee.email}
                  </div>
                  <Select
                    placeholder="Change Role of the employee"
                    options={roleOptions}
                    defaultValue={employee.role}
                    onChange={value =>
                      editRole(value.target.children[0].outerText, employee)
                    }
                  />
                  <button
                    class="negative ui button"
                    style={{ order: 2 }}
                    onClick={() => handleOpen(employee)}
                  >
                    Delete Employee
                  </button>
                </div>
              ))
            : "No Employees registered"}
        </Fragment>
      </div>
      <div>
        <Modal
          aria-labelledby="transition-modal-title"
          aria-describedby="transition-modal-description"
          className={classes.modal}
          open={open}
          onClose={handleClose}
          closeAfterTransition
          BackdropComponent={Backdrop}
          BackdropProps={{
            timeout: 500
          }}
        >
          <Fade in={open}>
            <div className={classes.paper}>
              <h1>Are you sure you want to delete this employee?</h1>
              <h2 id="transition-modal-title">Username: </h2>
              <hp>{selectedEmployee.userName}</hp>

              <h2 id="transition-modal-title">Email: </h2>
              <p id="transition-modal-description"> {selectedEmployee.email}</p>
              <h2 id="transition-modal-title">Role: </h2>
              <p id="transition-modal-description">{selectedEmployee.role}</p>
              <div
                class="ui red basic cancel  button"
                onClick={() => handleClose()}
              >
                <i class="remove icon"></i>
                No
              </div>
              <div
                class="ui green ok  button"
                onClick={() => removeEmployee(selectedEmployee)}
              >
                <i class="checkmark icon"></i>
                Yes
              </div>
            </div>
          </Fade>
        </Modal>
      </div>
    </Fragment>
  );
};
export default withRouter(Employees);
