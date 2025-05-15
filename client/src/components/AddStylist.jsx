import { useState } from "react";
import { createStylist } from "../services/StylistServices";


export default function AddStylists() {

const [name, setName] = useState("");

const handleSubmit = async(e) => {
    e.preventDefault();
    await createStylist({name});
    setName("");
}



  return (
   
  <form onSubmit={handleSubmit}>
    <h2>Add Stylists</h2>
    <label>
        Name: 
        <input type= "text" value={name} onChange={(event) => setName(event.target.value)}
        />
    </label>
<button type="submit">Add Stylists</button>
     </form>
)
}