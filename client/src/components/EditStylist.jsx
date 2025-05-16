import { useEffect, useState } from "react";
import { getStylists, editStylist } from "../services/StylistServices";

export default function EditStylist() {
  const [stylists, setStylists] = useState([]);
  const [isActive, setIsActive] = useState(true);
  const [stylist, setStylist] = useState("")
  
  useEffect(() => {
    getStylists().then(setStylists);
  }, []);

  const stylistChange = async (event) => {
    const id = parseInt(event.target.value);
    setStylist(id);
    const newStylist = stylists.find((s) => s.id == id);
    if (newStylist) {
    setIsActive(newStylist.isActive);
    }
  }

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    try {
      await editStylist(stylist, { isActive });
      alert("Stylist updated successfully!");
    } catch (error) {
      console.error(error);
      alert("There was an error updating the stylist.");
    }

  }

  return (
  <form onSubmit = {handleSubmit}>
    <h2>Edit Stylist</h2>

    <label>Select Stylist:</label>
    <select value={stylist} onChange={stylistChange}>
      <option value="">Choose Stylist</option>
        {stylists.map((s) => (
          <option key={s.id} value={s.id}>
            {s.name}
          </option>
        ))}
    </select>

    <label>Active Status</label>
    <input type="checkbox" value={true} checked={isActive === true} onChange={() => setIsActive (true)}></input>
    Active
    <input type="checkbox" value={false} checked={isActive === false} onChange={() => setIsActive (false)}></input>
    Inactive

    <button type="submit">Update Stylist</button>
  </form>
)}