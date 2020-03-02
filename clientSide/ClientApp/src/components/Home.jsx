import React, {  Fragment ,useContext} from "react";
import CardExample from "./CardExample";

import { CreateForm } from "./CreateForm";
import  UserContext  from "./../UserContext";
import {
  
  withRouter,

} from "react-router-dom";


const Home = ({removeUser,handleSubmit}) => {
  const dataContext = useContext(UserContext);


  return (
    <Fragment>
      <CreateForm handleSubmit={handleSubmit} />
      <CardExample removeUser={removeUser} />
    </Fragment>
  );
};
export default withRouter(Home);
