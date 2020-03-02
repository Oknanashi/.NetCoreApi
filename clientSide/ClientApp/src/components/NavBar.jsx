import React, { useEffect, useState } from "react";
import { Menu, Container, Button } from "semantic-ui-react";
import axios from "axios";
import { NavLink, useHistory } from "react-router-dom";
import styled from 'styled-components';

const JustyfiedContainer = styled.div`
  display:flex;
  justify-content:space-between;

`

export const NavBar = ({ accessor, setToken }) => {
  let history = useHistory();
  const [user, setUser] = useState();

  //Token bug

  useEffect(() => {
    axios
      .get("https://localhost:5001/api/user/currentUser", {
        headers: {
          Authorization: `Bearer ${window.localStorage.getItem("jwt")}`
        }
      })
      .then(data => {
        setUser(data.data);
        console.log(data.data);
      });
  }, []);
  const logoutUser = () => {
    window.localStorage.removeItem("jwt");
    setToken(null);
    setUser(null);
    history.push("/");
  };
  console.log(user)
  return (
    <Menu inverted>
      <Container>
        <JustyfiedContainer style={{minWidth:"100%"}}>
        <Menu.Item header as={NavLink} exact to="/">
          Reactivities
        </Menu.Item>
        <div  style={{display:"flex"}}>
          {accessor == null ? (
            <div>
              <Menu.Item>
                <Button
                  as={NavLink}
                  to="/register"
                  positive
                  content="Register"
                />
              </Menu.Item>
              <Menu.Item>
                <Button as={NavLink} to="/login" positive content="Login" />
              </Menu.Item>
            </div>
          ) : (
            <Menu.Item>
              <Menu.Item header as={NavLink} exact to="/employees">
          Manage Employees
        </Menu.Item>
              <Button
                positive
                content="Logout"
                onClick={() => {
                  logoutUser();
                }}
              />
            </Menu.Item>
          )}
          {user && (
            <div style={{display:"flex",alignItems:"center"}}>
              <a class="ui image label" fluid>
                {user.username} <br/>
                as {user.role}
              </a>
              
              
            </div>
          ) }
          </div>
        
        </JustyfiedContainer>
      </Container>
    </Menu>
  );
};
