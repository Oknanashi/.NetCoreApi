import React, { useContext, useEffect, useState } from "react";
import { Grid, Segment,Container } from "semantic-ui-react";
import UserContext from "./../UserContext";
import { withRouter } from "react-router-dom";
import styled from 'styled-components';

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

  let userDetails = userStore.filter(user => user.Id === match.params.id);

  useEffect(() => {
    setUser(userDetails);
  }, []);
  console.log(userDetails[0]);
  return (
    <div>
      {userDetails[0] != undefined ? (
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
          <Segment fluid  >
            <p >{userDetails[0].Bio}</p>
          </Segment>
        </WrappingComponent>
      ) : (
        "none"
      )}
    </div>
  );
};
export default withRouter(UserDetails);
