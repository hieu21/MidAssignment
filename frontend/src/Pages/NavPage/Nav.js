import { Layout, Menu, Breadcrumb } from "antd";
import { Link } from "react-router-dom";
import { useContext } from "react";
import UserContext from "../../Context/UserContext";
import CartContext from "../../Context/CartContext";
const Nav = () => {
  const { Header } = Layout;
  const { currentUser, setCurrentUser } = useContext(UserContext);
  const { cart } = useContext(CartContext);

  const handleLogout = () => {
    localStorage.removeItem("token");
    setCurrentUser(null);
  };

  return (
    <div>
      {currentUser && currentUser.role === 1 && (
        <Header>
          <div className="logo" />
          <Menu theme="dark" mode="horizontal" defaultSelectedKeys={["1"]}>
            <Menu.Item key={1}>
              <Link to="/">Home</Link>
            </Menu.Item>
            <Menu.Item key={2}>
              <Link to="/borrowedBooks">Status Books</Link>
            </Menu.Item>
            <Menu.Item key={3}>
              <Link to="/bookcart">Cart ({cart && cart.length})</Link>
            </Menu.Item>
            <Menu.Item key={4}>
              <Link to="/logout" onClick={handleLogout}>
                Logout
              </Link>
            </Menu.Item>
          </Menu>
        </Header>
      )}
      {currentUser && currentUser.role === 0 && (
        <Header>
          <div className="logo" />
          <Menu theme="dark" mode="horizontal" defaultSelectedKeys={["1"]}>
            <Menu.Item key={1}>
              <Link to="/admin">Book Manager</Link>
            </Menu.Item>
            <Menu.Item key={2}>
              <Link to="/admin/categoryManager">Category Manager</Link>
            </Menu.Item>
            <Menu.Item key={3}>
              <Link to="/admin/borrowManager">Borrow Manager</Link>
            </Menu.Item>
            <Menu.Item key={4}>
              <Link to="/logout" onClick={handleLogout}>
                Logout
              </Link>
            </Menu.Item>
          </Menu>
        </Header>
      )}
      {currentUser ? (
        ""
      ) : (
        <Link to="/login">Login</Link>
      )}
      {}
    </div>
  );
};
export default Nav;
