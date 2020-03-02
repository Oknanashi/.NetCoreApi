import React, {  useState } from "react";

import { Form, Button } from "semantic-ui-react";
import { withRouter } from "react-router-dom";

const LoginForm = ({loginUser}) => {
  const [userValue, setUserValue] = useState({});

  const submitLogin = (userValue)=>loginUser(userValue);
  return (
    <Form onSubmit={()=>submitLogin(userValue)}>
      
      <Form.Field> 
        <label>Email</label>
        <input
          placeholder="Email"
          type="Email"
          onChange={e => (userValue.Email = e.currentTarget.value)}
        />
      </Form.Field>
      
      <Form.Field>
        <label>Password</label>
        <input
          type="password"
          placeholder="Your password"
          onChange={e => (userValue.Password = e.currentTarget.value)}
        />
      </Form.Field>

      <Button type="submit">Submit</Button>
    </Form>
  );
};

export default withRouter(LoginForm);
