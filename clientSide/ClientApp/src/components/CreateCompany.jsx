import React,{useState} from "react";
import {Form,Button} from 'semantic-ui-react';

export const CreateCompany = ({handleSubmit}) => {
    const [companyValue,setcompanyValue] = useState({});

    const finalSubmit =()=>handleSubmit(companyValue,"Company");
    
  return (
    <Form onSubmit={finalSubmit}>
      <Form.Field>
        <label>Company Name</label>
        <input placeholder="Company Name" value={companyValue.CompanyName}
        
        onChange={(e)=>companyValue.CompanyName = e.currentTarget.value}/>
      </Form.Field>
      <Form.Field>
        <label>Address</label>
        <input placeholder="Address" value={companyValue.Address}
        onChange={(e)=>companyValue.Address = e.currentTarget.value}/>
      </Form.Field>
      <Form.Field>
        <label>Company Sector(select maybe?)</label>
        <input  placeholder="Company Sector" 
        onChange={(e)=>companyValue.CompanySector = e.currentTarget.value}
        value={companyValue.CompanySector}/>
      </Form.Field>
      
      <Button type="submit">Submit</Button>
    </Form>
  );
};
