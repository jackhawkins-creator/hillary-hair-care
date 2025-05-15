import { useState } from "react";
import { createCustomer } from "../services/CustomerServices";


export default function AddCustomer() {

const [name, setName] = useState("");

const handleSubmit = async(e) => {
    e.preventDefault();
    await createCustomer({name});
    setName("");
}



  return (
   
  <form onSubmit={handleSubmit}>
    <h2>Add Customer</h2>
    <label>
        Name: 
        <input type= "text" value={name} onChange={(event) => setName(event.target.value)}
        />
    </label>
<button type="submit">Add Customer</button>
     </form>
)
} 
