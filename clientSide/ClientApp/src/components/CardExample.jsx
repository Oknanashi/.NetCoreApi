import React from "react";
import { Card, Icon, Image } from "semantic-ui-react";
import { Link} from "react-router-dom";
import styled from "styled-components";
import {UserConsumer} from "./../UserContext";

const CenteredRow = styled.div`
  margin-top: 5em;
  font-size: 1.5em;
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  justify-content: space-evenly;
  .ui.card {
    margin: 0;
    .image {
      position: static;
    }
  }
  .delete {
    position: absolute;
  }
`;

const CardExample = ({removeUser}) => {
  
  

 
  

  
  
  return (
    <UserConsumer>
      
      {context => 
      
      {
        
      return(<CenteredRow>
       {context.users  ? 
        context.users.map(user => (
          <Card key={user.Id}>
            <Icon
              bordered
              inverted
              color="teal"
              name="delete"
              onClick={() => removeUser(user)}
            />
            <Image
            as={Link} to={`/user/${user.Id}`}
              src="https://react.semantic-ui.com/images/avatar/large/matthew.png"
              wrapped
              ui={false}
              // onClick={() => goToUser(user)}
              
            />
            <Card.Content>
              <Card.Header>{user.FirstName}  {user.LastName}</Card.Header>
              <Card.Meta>
                <span className="date">Age : {user.Age}</span>
                <br></br>
                <span className="date">Registered: {user.CreatedAt}</span>
              </Card.Meta>
              <Card.Description>{user.Bio}</Card.Description>
              {user.EmployeeCompany != null && (<Card.Description>{user.EmployeeCompany.CompanyName}</Card.Description>)}
            </Card.Content>
            <Card.Content extra>
              <a>
                <Icon name="user" />
                22 Friends
              </a>
            </Card.Content>
          </Card>
        )) : "Empty users"}
    </CenteredRow>)}}
    </UserConsumer>
    
  );
};

export default CardExample;
