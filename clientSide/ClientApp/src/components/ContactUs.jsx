import React,{useState} from "react";
import axios from 'axios';
import {useHistory} from 'react-router-dom';

export const ContactUs = () => {
    const [mailValues,setMailValues] = useState({});
    let history = useHistory();

    const submitMail=()=>{
        axios.post("https://localhost:5001/api/user/sendemail",mailValues).then((response)=>{
            console.log(response);
            // history.push('/');
        }).catch(e=>console.log(e))
    }
  return (
    <form class="ui form"  onSubmit={submitMail}>
      <div class="field">
        <label>Your Name</label>
        <input type="text" name="name" placeholder="Name" value={mailValues.name}
        onChange={(e)=>mailValues.name = e.currentTarget.value}/>
      </div>
      <div class="field">
        <label>Email</label>
        <input type="email" name="email" placeholder="Email"value={mailValues.email} 
        onChange={(e)=>mailValues.email = e.currentTarget.value}/>
      </div>
      <div class="field">
        <label>Subject </label>
        <input type="text" name="Subject" placeholder="Subject"value={mailValues.Subject} 
        onChange={(e)=>mailValues.Subject = e.currentTarget.value}/>
      </div>
      <div class="field">
        <label>Message </label>
        <textarea type="text" name="msg" placeholder="Message" value={mailValues.msg}
        onChange={(e)=>mailValues.msg = e.currentTarget.value}/>
      </div>
      
      <button class="ui button" type="submit">
        Submit
      </button>
    </form>
  );
};
