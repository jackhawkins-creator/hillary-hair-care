import { getAppointments, deleteAppointment,createAppointment} from "../services/AppointmentServices";
import { getCustomers } from "../services/CustomerServices";
import { getStylists } from "../services/StylistServices";
import { getServices } from "../services/ServicesServices";
import { useEffect, useState } from "react";

export default function Homepage() {
const [appointments, setAppointments] = useState ([]);
const [customers, setCustomers] = useState ([]);
const [services, setServices] = useState ([]);
const [ stylists, setStylists] = useState ([]);
const [ newAppointment, setNewAppointment] = useState (
    {   stylistId: "",
        customerId: "",
        time: "",
        serviceIds: [], 
    }
) 

useEffect (() => {
    fetchData ();
}, [])

const fetchData = async ()=> {
    const [app,sty,cust,serv] = await Promise.all(
      [getAppointments(),
      getCustomers(),
      getStylists(),
      getServices()  
    ]);
    setAppointments(app);
    setCustomers(cust);
    setServices(serv);
    setStylists(sty);
}

const handleDelete = async (id) => {
    await deleteAppointment(id);
    fetchData();
}

const handleInputChange = (key, value) => {
  setNewAppointment((prev) => ({
    ...prev,
    [key]: value,
  }));
};





return (
    <div>
        <h1> Create a new Appointment</h1>
        <label> Stylist:</label>
        <select 
        value={newAppointment.stylistId}
        onChange={(event) => handleInputChange("stylistId",parseInt(event.target.value))} >
<option value= ""> Select a stylist </option>
    {stylists.map((s) => (
        <option key ={s.id} value={s.id}>
            {s.name}
        </option>
    ))}
        </select>
<label> Customer:</label>
        <select 
        value={newAppointment.customerId}
        onChange={(event) => handleInputChange("customerId",parseInt(event.target.value))} >
<option value= ""> Select a customer </option>
    {customers.map((c) => (
        <option key ={c.id} value={c.id}>
            {c.name}
        </option>
    ))}
</select>
<label> Time:</label>
        <input
        type="datetime-local"
        value={newAppointment.time}
        onChange={(event) => handleInputChange("time",(event.target.value))} >
        </input>


        


    </div>
)





} 





//get all appointments
// delete appointments
//create appointments