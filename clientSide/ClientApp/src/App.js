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

import Employees from './components/Employees';
import { ContactUs } from './components/ContactUs';
import { ToastContainer, toast } from 'react-toastify';



const App = ()=> {
  
  //Token bug

  const [users, setUsers] = useState([]);
  let history = useHistory();

  const [accessor,setAccessor] = useState(null)

  const [userValues,setUserValues]=useState('')
  
  const setConfig = {headers:{Authorization: `Bearer ${window.localStorage.getItem("jwt")}`}}
  

  useEffect(() => {
     getUsers();
    const token = window.localStorage.getItem("jwt")
    
    
    setToken(token)
    
  }, [accessor]);

  const notifyErrors = (errors)=>{
    if(typeof errors =="string"){
      toast.error(errors)
    }
    else if(Object.entries(errors).length>1){
      {for (var [key,value] of Object.entries(errors)){
        toast.error(value[0])
    }}
    } else if(Object.entries(errors).length===1){
      
      toast.error(errors[Object.keys(errors)[0]])
    }else {
      toast.error("Undefined error")
    }
  }

  const notifySuccess = (message)=>{
      toast.success(message);
  }

  const setToken= (value)=>{
    setAccessor(value)
  }

  const getUsers = () => {
    axios.get("https://localhost:5001/api/home",setConfig).then(data => {
      setUsers(data.data);
    });
  };

  const removeUser = user => {
    
    axios.delete(`https://localhost:5001/api/home/${user.Id}`,setConfig).then(data => {
      
      axios.get("https://localhost:5001/api/home",setConfig).then(data => {
        
        setUsers(data.data);
        notifySuccess("User was deleted");
      });
    }).catch(e=>notifyErrors(e.response.data.errors));
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
          Bio: values.Bio,
          Company:values.Company
        };
        
        axios
          .post("https://localhost:5001/api/home/register", newUser,setConfig)
          .then(response => {
            console.log(response)
            getUsers();
            notifySuccess("User was created");
          })
          .catch(e=>notifyErrors(e.response.data.errors));
          break;
      case "Company":
        
        var newCompany = {
          CompanyName: values.CompanyName,
          Address: values.Address,
          CompanySector: values.CompanySector
          
        };
        axios
          .post("https://localhost:5001/api/company/createcompany", newCompany,setConfig)
          .then(response => {
    
            getUsers();
            notifySuccess("Company was created");
            console.log(response.data)
          })
          .catch(e=>notifyErrors(e.response.data.errors));
          break;
    } 
    }

    const registerUser=(userValue)=>{
        setUserValues(userValue)
        axios.post("https://localhost:5001/api/user/register",userValue,setConfig)
        .then(response=>{
          window.localStorage.setItem("jwt",response.data.token)
          history.push('/')
          notifySuccess("You were successfully registered");
        }).catch(e=>{
          console.log(e.response.data.errors)
          notifyErrors(e.response.data.errors)
        })
    }

    const loginUser=(userValue)=>{
      
      axios.post("https://localhost:5001/api/user/login",userValue,setConfig)
      .then(response=>{
        console.log(response)
        window.localStorage.setItem("jwt",response.data.token)
        setToken(window.localStorage.setItem("jwt",response.data.token))
        history.push('/')
        notifySuccess("You were successfully logged in");
      }).catch(e=>{
        notifyErrors(e.response.data.errors)
      })
  }
    
    return (
      <UserProvider value={{users,notifySuccess,notifyErrors,setConfig}}  >
        <Container fluid="true">         
          <NavBar accessor={accessor} setToken={setToken}/>
          <ToastContainer autoClose={2000}/>
          
          <Route exact path='/' component={() => <Home  handleSubmit={handleSubmit} removeUser={removeUser}/>}/>
          <Switch>
          <Route exact path="/user/:id" component={UserDetails} />
          <Route exact path="/register" component={()=><Register registerUser={registerUser} userValues={userValues}/>} />
          <Route exact path="/login" component={()=><Login loginUser={loginUser}/>} />
          <Route exact path="/employees" component={()=><Employees/>} />
          <Route exact path="/contactus" component={()=><ContactUs/>} />
          </Switch>
        </Container>
        
        </UserProvider>
      
    );
    
  };
export default withRouter(App);