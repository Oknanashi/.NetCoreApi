import React, { useEffect, useState } from "react";
import {  Segment } from "semantic-ui-react";

import MenuItem from '@material-ui/core/MenuItem';

import Select from '@material-ui/core/Select';

import axios from 'axios'


export const CompaniesSelect = ({ userDetails,onChangeCompany,type }) => {
    // const [user, setUser] = useState({});

  const [companies, setCompanies] = useState();
  const [company, setCompany] = useState();
//   const [hasChanged, setChanged] = useState(false);

  useEffect(() => {
    axios
      .get("https://localhost:5001/api/company", {
        headers: {
          Authorization: `Bearer ${window.localStorage.getItem("jwt")}`
        }
      })
      .then(data => {
        console.log(data.data.map(value => value.CompanyName));
        setCompanies(data.data);
        
      });
    
        
    
  }, []);

  
  console.log(type)
  
  return (
    <div>
        {companies && (<Segment>
      <h1>Companies</h1>
      {/* <Select placeholder='Change Role of the employee' options={companies} defaultValue={"Microsoft"}  /> */}

      {type==="empty" || userDetails[0].EmployeeCompany===null ?(<Select
        style={{ minWidth: "100%" }}
        
        labelId="demo-simple-select-label"
        id="demo-simple-select"
        value={company}
        onChange={onChangeCompany}
        
      >
        {companies.map(company => (
          <MenuItem key={company.CompanyName} value={company.CompanyName}>{company.CompanyName}</MenuItem>
        ))}
      </Select>) :( 
          <Select
          style={{ minWidth: "100%" }}
          
          labelId="demo-simple-select-label"
          id="demo-simple-select"
          value={company}
          onChange={onChangeCompany}
          defaultValue={userDetails[0].EmployeeCompany.CompanyName}
        >
          {companies.map(company => (
            <MenuItem key={company.CompanyName} value={company.CompanyName}>{company.CompanyName}</MenuItem>
          ))}
        </Select>
      )}
    </Segment>)}
    </div>
  );
};
