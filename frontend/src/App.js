import "./App.css";
import Login from "./Pages/LoginPage/Login";
import { useEffect, useState } from "react";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Redirect,
} from "react-router-dom";
import Nav from "./Pages/NavPage/Nav";
import UserContext from "./Context/UserContext";
import CartContext from "./Context/CartContext";
import ListBookManager from "./Pages/AdminPage/BookManager/ListBookManager";
import ListCategoryManager from "./Pages/AdminPage/CategoryManager/ListCategoryManager";
import EditBookManager from "./Pages/AdminPage/BookManager/EditBookManager";
import ListBorrowManager from "./Pages/AdminPage/BorrowManager/ListBorrowManager";
import AddBookManager from "./Pages/AdminPage/BookManager/AddBookManger";
import AddCategoryManager from "./Pages/AdminPage/CategoryManager/AddCategoryManager";
import EditCategoryManager from "./Pages/AdminPage/CategoryManager/EditCategoryManager";
import ListBook from "./Pages/UserPage/ListBook";
import ListStatus from "./Pages/UserPage/ListStatus";
import ListBorrow from "./Pages/UserPage/ListBorrow";
function App() {
  const [currentUser, setCurrentUser] = useState(null);
  const [cart, setCart] = useState([]);

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      setCurrentUser(JSON.parse(token));
    }
  }, []);

  useEffect(() => {
    const cart = localStorage.getItem("cart");
    if (cart) {
      setCart(JSON.parse(cart));
    }
  }, []);

  let userLogin = null;
  let routeLink = null;
  if (currentUser !== null) {
    if (currentUser.role === 0) {
      userLogin = <Redirect to="/admin" />;
      routeLink = (
        <>
          <Route exact path="/admin">
            <ListBookManager />
          </Route>
          <Route path="/admin/addBook">
            <AddBookManager />
          </Route>
          <Route path="/admin/books/:bookId/edit">
            <EditBookManager />
          </Route>
          <Route path="/admin/addCategory">
            <AddCategoryManager />
          </Route>
          <Route path="/admin/categories/:categoryId/edit">
            <EditCategoryManager />
          </Route>
          <Route path="/admin/borrowManager">
            <ListBorrowManager />
          </Route>
          <Route path="/admin/categoryManager">
            <ListCategoryManager />
          </Route>
        </>
      );
    } else if (currentUser.role === 1) {
      userLogin = <Redirect to="/" />;
      routeLink = (
        <>
          <Route path="/borrowedBooks">
            <ListStatus />
          </Route>
          <Route path="/bookcart">
            <ListBorrow />
          </Route>
          <Route exact path="/">
            <ListBook />
          </Route>
        </>
      );
    }
  } else {
    userLogin = <Login />;
  }
  return (
    <div>
      <Router>
        <UserContext.Provider value={{ currentUser, setCurrentUser }}>
          <CartContext.Provider value={{ cart, setCart }}>
            <Nav />
            <Switch>
              <Route path="/login">{userLogin}</Route>
              {routeLink}
            </Switch>
          </CartContext.Provider>
        </UserContext.Provider>
      </Router>
    </div>
  );
}

export default App;
