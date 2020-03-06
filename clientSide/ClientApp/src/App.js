import React, { useState,useEffect } from 'react';
import { Container } from "react-bootstrap";
import  Home  from './components/Home';
import Register from './components/Auth/Register'
import Login from './components/Auth/Login'
import {NavBar} from './components/NavBar';
import {
  
  Route,
  withRouter,
  
  Switch,

} from 'react-router-dom';
import {useHistory} from 'react-router-dom';
import axios from 'axios'
import './custom.css'
import UserDetails from './components/UserDetails'
import {UserProvider} from './UserContext'
import ErrorSegment from './components/ErrorSegment';
import Employees from './components/Employees';

const App = ()=> {
  
  //Token bug

  const [users, setUsers] = useState([]);
  let history = useHistory();

  const [accessor,setAccessor] = useState(null)
  const [errors,setErrors] = useState([null])
  const [userValues,setUserValues]=useState('')
  

  

  useEffect(() => {
     getUsers();
    const token = window.localStorage.getItem("jwt")
    
    
    setToken(token)
    
  }, [accessor]);

  const setToken= (value)=>{
    setAccessor(value)
  }

  const getUsers = () => {
    axios.get("https://localhost:5001/api/home",{headers:{Authorization: `Bearer ${window.localStorage.getItem("jwt")}`}}).then(data => {
      setUsers(data.data);
    });
  };

  const removeUser = user => {
    console.log(user);
    axios.delete(`https://localhost:5001/api/home/${user.Id}`,{headers:{Authorization: `Bearer ${window.localStorage.getItem("jwt")}`}}).then(data => {
      console.log(data);
      axios.get("https://localhost:5001/api/home",{headers:{Authorization: `Bearer ${window.localStorage.getItem("jwt")}`}}).then(data => {
        console.log(data.data);
        setUsers(data.data);
      });
    });
  };

  // const submitCompany = () =>{
  //   console.log(userDetails[0].Id,companies.find(obj=>obj.CompanyName==company).Id);
  //   var userValues = {
  //     UserId:userDetails[0].Id,
  //     CompanyId:companies.find(obj=>obj.CompanyName==company).Id
  //   };
  //   axios.post("https://localhost:5001/api/home/updateuser",userValues,{headers:{Authorization: `Bearer ${window.localStorage.getItem("jwt")}`}})
  //       .then(response=>{
  //         console.log(response)
  //         setChanged(false);
  //       }).catch(e=>{
  //         console.log(e)
  //       })
    
  // }
  

  const handleSubmit = (values,type) => {
    switch(type){
      case "User":
        var newUser = {
          FirstName: values.FirstName,
          LastName: values.LastName,
          Age: parseInt(values.Age),
          Bio: values.Bio
        };
        axios
          .post("https://localhost:5001/api/home/register", newUser)
          .then(response => {
    
            getUsers();
          })
          .catch(e=>console.log(e));
      case "Company":
        var newCompany = {
          CompanyName: values.CompanyName,
          Address: values.Address,
          CompanySector: values.CompanySector
          
        };
        axios
          .post("https://localhost:5001/api/company/createcompany", newCompany)
          .then(response => {
    
            getUsers();
            console.log(response.data)
          })
          .catch(e=>console.log(e));
    } 
    }

    const registerUser=(userValue)=>{
        setUserValues(userValue)
        axios.post("https://localhost:5001/api/user/register",userValue)
        .then(response=>{
          window.localStorage.setItem("jwt",response.data.token)
          history.push('/')
        }).catch(e=>{
          setErrors(e.response.data.errors)
        })
    }

    const loginUser=(userValue)=>{
      
      axios.post("https://localhost:5001/api/user/login",userValue)
      .then(response=>{
        console.log(response)
        window.localStorage.setItem("jwt",response.data.token)
        setToken(window.localStorage.setItem("jwt",response.data.token))
        history.push('/')
      }).catch(e=>{
        setErrors(e.response.data.errors)
      })
  }
    console.log(users)
    return (
      <UserProvider value={users} handleSubmit={handleSubmit}  >
        <Container fluid="true">
         
          <NavBar accessor={accessor} setToken={setToken}/>
          <ErrorSegment errors={errors}/>
          <Route exact path='/' component={() => <Home  handleSubmit={handleSubmit} removeUser={removeUser}/>}/>
          <Switch>
          <Route exact path="/user/:id" component={UserDetails} />
          <Route exact path="/register" component={()=><Register registerUser={registerUser} userValues={userValues}/>} />
          <Route exact path="/login" component={()=><Login loginUser={loginUser}/>} />
          <Route exact path="/employees" component={()=><Employees/>} />
          </Switch>
        </Container>
        
        </UserProvider>
      
    );
    
  };
export default withRouter(App);