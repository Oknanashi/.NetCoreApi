import React, { useContext, useEffect, useState } from "react";
import { Grid, Segment, Container,Select } from "semantic-ui-react";
import UserContext from "./../UserContext";
import { withRouter } from "react-router-dom";
import styled from "styled-components";
import axios from "axios";

const WrappingComponent = styled.div`
  p {
    overflow-wrap: break-word;
    word-wrap: break-word;
    hyphens: auto;
  }
`;

const roleOptions = [
    { key: 'Admin', value: 'Admin', text: 'Admin' },
    { key: 'Member', value: 'Member', text: 'Member' },
    { key: 'Manager', value: 'Manager', text: 'Manager' },
   
  ]

const Employees = ({ match, history }) => {
  const userStore = useContext(UserContext);
  const [employees, setEmployees] = useState();
  const [employeeRole,setRole]=useState();


  useEffect(() => {
    axios
      .get("https://localhost:5001/api/user/getemployees", {
        headers: {
          Authorization: `Bearer ${window.localStorage.getItem("jwt")}`
        }
      })
      .then(data => {
        setEmployees(data.data);
        console.log(data);
      });
  }, []);

  const editRole = (role,employee)=>{
    var userValue = {
      role:role,
      email:employee.email
    };
    axios.post("https://localhost:5001/api/user/editEmployeeRole",userValue,{headers:{Authorization: `Bearer ${window.localStorage.getItem("jwt")}`}})
        .then(response=>{
          console.log(response)
        }).catch(e=>{
          console.log(e)
        })
  } 


  console.log(employees);
  return (
    <div class="ui celled list">
      {employees ? employees.map(employee=>(
          <div class="item" style={{display:"flex",WebkitJustifyContent:"space-between"}} key={employee.id}>
          <div class="content">
            <div class="header">{employee.userName}</div>
            {employee.email}
          </div>
          <Select placeholder='Change Role of the employee' options={roleOptions} defaultValue={employee.role} onChange={value=>editRole(value.target.children[0].outerText,employee)} />
          
        </div>
      )): ("No Employees registered")}
    </div>
  );
};
export default withRouter(Employees);
