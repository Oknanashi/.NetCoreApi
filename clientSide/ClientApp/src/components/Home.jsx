import React, { Fragment, useContext } from "react";
import CardExample from "./CardExample";

import { CreateForm } from "./CreateForm";
import { CreateCompany } from "./CreateCompany";
import { withRouter } from "react-router-dom";

const Home = ({ removeUser, handleSubmit }) => {
  
  
  return (
    <Fragment>
      <div style={{ display: "flex", justifyContent: "space-around" }}>
        <CreateForm handleSubmit={handleSubmit} />
        <CreateCompany handleSubmit={handleSubmit} />
      </div>
      <CardExample removeUser={removeUser} />
    </Fragment>
  );
};
export default withRouter(Home);
