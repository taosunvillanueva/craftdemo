import React from 'react';
import logo from './logo.svg';
import './App.css';
import RegisterForm from './RegisterForm'


function App() {
  return (
    <div className="App">
      <RegisterForm />


      {/* <Route path="/" component={RegisterForm} /> */}
      {/* <Route path="/thankyou" component ={Thankyou} />   */}

      {/* <RegisterForm />
      <div>
        <Route path="/thankyou" component ={Thankyou} />  
      </div> */}
    </div>
  );
}

export default App;
