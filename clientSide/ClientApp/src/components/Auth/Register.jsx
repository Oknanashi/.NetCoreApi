import React, { useState,useEffect } from "react";

import { Form, Button } from "semantic-ui-react";
import { withRouter } from "react-router-dom";

const RegisterForm = ({ registerUser, userValues }) => {
  const [userValue, setUserValue] = useState({});

  const submitRegister = userValue => registerUser(userValue);
  useEffect(() => {
    console.log(userValues);
    const userInput = {
      Email: userValues.Email || "",
      Username: userValues.Username || "",
      Password: userValues.Password || ""
    };
    setUserValue(userInput) 
  },[]);
  return (
    <Form onSubmit={() => submitRegister(userValue)}>
      <Form.Field>
        <label>Email</label>
        <input
          placeholder="Email"
          type="email"
          defaultValue={userValue.Email}
          onChange={e => (userValue.Email = e.currentTarget.value)}
        />
      </Form.Field>
      <Form.Field>
        <label>Username</label>
        <input
          placeholder="Username"
          defaultValue={userValue.Username}
          onChange={e => (userValue.Username = e.currentTarget.value)}
        />
      </Form.Field>
      <Form.Field>
        <label>Password</label>
        <input
          type="password"
          placeholder="Your password"
          defaultValue={userValue.Password}
          onChange={e => (userValue.Password = e.currentTarget.value)}
        />
      </Form.Field>

      <Button type="submit">Submit</Button>
    </Form>
  );
};

export default withRouter(RegisterForm);
