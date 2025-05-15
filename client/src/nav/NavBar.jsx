import { Link } from "react-router-dom";

export default function Navbar() {
  return (
    <nav>
      <Link to="/">Home</Link>
      <Link to="/add-stylist">Add Stylist</Link>
      <Link to="/add-customer">Add Customer</Link>
      <Link to="/edit-stylist">Edit Stylist</Link>
    </nav>
  );
}
