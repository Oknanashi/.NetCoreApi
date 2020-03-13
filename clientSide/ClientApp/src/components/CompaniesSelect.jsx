import React, { useEffect, useState } from "react";
import {  Segment } from "semantic-ui-react";

import MenuItem from '@material-ui/core/MenuItem';

import Select from '@material-ui/core/Select';

import axios from 'axios'


export const CompaniesSelect = ({ userDetails,onChangeCompany,type,finalSubmit }) => {
    // const [user, setUser] = useState({});

  const [companies, setCompanies] = useState();
  
  const [company, setCompany] = useState();
//   const [hasChanged, setChanged] = useState(false);
  

  useEffect(() => {
    let isCancelled = false;
    axios
      .get("https://localhost:5001/api/company", {
        headers: {
          Authorization: `Bearer ${window.localStorage.getItem("jwt")}`
        }
      })
      .then(data => {
        // console.log(data.data.map(value => value.CompanyName));
        if(!isCancelled){
          setCompanies(data.data);
        }
        
        
      });
      return () => {
        isCancelled = true;
      };
    
        
    
  }, []);

  
  
  
  return (
    <div>
        {companies && (<Segment>
      <h1>Companies</h1>
      {/* <Select placeholder='Change Role of the employee' options={companies} defaultValue={"Microsoft"}  /> */}

      {type==="empty" || userDetails[0].EmployeeCompany===null ?(
      <Select
        style={{ minWidth: "100%" }}
        defaultValue="DEFAULT"
        labelId="demo-simple-select-label"
        id="demo-simple-select"
        value={company}
        onChange={onChangeCompany,(e)=>finalSubmit(companies.find(x=>x.CompanyName===e.target.value))}
        
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
