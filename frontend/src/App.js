import "./App.css";
import Login from "./Pages/LoginPage/Login";
import { useEffect, useState } from "react";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Redirect,
} from "react-router-dom";
import UserContext from "./Context/UserContext";
function App() {
  const [currentUser, setCurrentUser] = useState(null);
  return (
    <div>
      <UserContext.Provider value={{ currentUser, setCurrentUser }}>
        <Login />
      </UserContext.Provider>
    </div>
  );
}

export default App;
