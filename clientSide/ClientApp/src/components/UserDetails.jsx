import React, { useContext, useEffect, useState } from "react";
import { Grid, Segment } from "semantic-ui-react";

import Button from '@material-ui/core/Button';

import UserContext from "./../UserContext";
import { withRouter } from "react-router-dom";
import styled from 'styled-components';
import axios from 'axios'
import {CompaniesSelect} from './CompaniesSelect'
// import { Button } from "react-bootstrap";

const WrappingComponent = styled.div`
    p{
        overflow-wrap: break-word;
  word-wrap: break-word;
  hyphens: auto;
    }
`

const UserDetails = ({ match, history }) => {
  const userStore = useContext(UserContext);
  const [user, setUser] = useState({});
  const [companies,setCompanies]=useState();
  const [company,setCompany]=useState();
  const [hasChanged,setChanged]=useState(false);

  let userDetails = userStore.filter(user => user.Id === match.params.id);

  useEffect(() => {
    axios.get("https://localhost:5001/api/company",{headers:{Authorization: `Bearer ${window.localStorage.getItem("jwt")}`}}).then(data => {
      console.log(data.data.map(value=>value.CompanyName))
      setCompanies(data.data);
    });
    setUser(userDetails);
  }, []);


  const onChangeCompany=(hasChanged,company)=>{
    console.log(userDetails[0])
    if(company.props.value!=null || company.props.value!=userDetails[0].EmployeeCompany.CompanyName){
      setChanged(true);
      setCompany(hasChanged.target.value)
    }
    
  }

  const submitCompany = () =>{
    console.log(userDetails[0].Id,companies.find(obj=>obj.CompanyName===company).Id);
    var userValues = {
      UserId:userDetails[0].Id,
      CompanyId:companies.find(obj=>obj.CompanyName===company).Id
    };
    axios.post("https://localhost:5001/api/home/updateuser",userValues,{headers:{Authorization: `Bearer ${window.localStorage.getItem("jwt")}`}})
        .then(response=>{
          console.log(response)
          setChanged(false);
        }).catch(e=>{
          console.log(e)
        })
    
  }
  console.log(userDetails,companies);
  return (
    <div>
      {userDetails[0] !== undefined && companies !== undefined ? (
        <WrappingComponent >
          <Segment >
            <Grid.Column width={10}>
              {/* 123  {singleUser.FirstName} */}
            </Grid.Column>
            <Grid.Column centered width={6}>
              <h2>
                {userDetails[0].FirstName} {userDetails[0].LastName}
              </h2>
            </Grid.Column>
          </Segment>
          <Segment fluid="true"  >
            <p >{userDetails[0].Bio}</p>
          </Segment>
          <CompaniesSelect userDetails={userDetails} onChangeCompany={onChangeCompany}/>
          <Segment>
            {hasChanged && (<Button variant="contained" color="primary" onClick={submitCompany}>
  Save Changes
</Button>)}
          </Segment>
        </WrappingComponent>
      ) : (
        "none"
      )}
    </div>
  );
};
export default withRouter(UserDetails);
