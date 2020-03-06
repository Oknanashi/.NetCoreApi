import React,{useState} from "react";
import {Form,Button} from 'semantic-ui-react';
import { CompaniesSelect } from "./CompaniesSelect";

export const CreateForm = ({handleSubmit}) => {
    const [userValue,setUserValue] = useState({});

    const finalSubmit =()=>handleSubmit(userValue,"User");
    
  return (
    <Form onSubmit={finalSubmit}>
      <Form.Field>
        <label>First Name</label>
        <input placeholder="First Name" value={userValue.FirstName}
        
        onChange={(e)=>userValue.FirstName = e.currentTarget.value}/>
      </Form.Field>
      <Form.Field>
        <label>Last Name</label>
        <input placeholder="Last Name" value={userValue.LastName}
        onChange={(e)=>userValue.LastName = e.currentTarget.value}/>
      </Form.Field>
      <Form.Field>
        <label>Age</label>
        <input type="number" placeholder="Your age" 
        onChange={(e)=>userValue.Age = e.currentTarget.value}
        value={userValue.Age}/>
      </Form.Field>
      <Form.Field>
        <label>Bio</label>
        <textarea placeholder="Your bio" 
        onChange={(e)=>userValue.Bio = e.currentTarget.value}
        value={userValue.Bio}/>
      </Form.Field>
      <CompaniesSelect type={"empty"}/>
      <Button type="submit">Submit</Button>
    </Form>
  );
};
