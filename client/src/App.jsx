import { Routes, Route } from "react-router-dom";
import Navbar from "./nav/NavBar";
import Homepage from "./components/Homepage";
import AddCustomer from "./components/AddCustomer";
import EditStylist from "./components/EditStylist";
import AddStylist from "./components/AddStylist";


function App() {
  return (
    <>
      <Navbar />

      <main>
        <Routes>
          <Route path="/" element={<Homepage />} />
          <Route path="/add-stylist" element={<AddStylist />} />
          <Route path="/add-customer" element={<AddCustomer />} />
          <Route path="/edit-stylist" element={<EditStylist />} />
        </Routes>
      </main>
    </>
  );
}

export default App;
