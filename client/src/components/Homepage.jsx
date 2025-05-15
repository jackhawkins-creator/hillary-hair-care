import {
  getAppointments,
  deleteAppointment,
  createAppointment,
} from "../services/AppointmentServices";
import { getCustomers } from "../services/CustomerServices";
import { getStylists } from "../services/StylistServices";
import { getServices } from "../services/ServicesServices";
import { useEffect, useState } from "react";

export default function Homepage() {
  const [appointments, setAppointments] = useState([]);
  const [customers, setCustomers] = useState([]);
  const [services, setServices] = useState([]);
  const [stylists, setStylists] = useState([]);
  const [newAppointment, setNewAppointment] = useState({
    stylistId: "",
    customerId: "",
    time: "",
    serviceIds: [],
  });

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    const [app, sty, cust, serv] = await Promise.all([
      getAppointments(),
      getCustomers(),
      getStylists(),
      getServices(),
    ]);
    setAppointments(app);
    setCustomers(cust);
    setServices(serv);
    setStylists(sty);
  };

  const handleDelete = async (id) => {
    setAppointments((a) => a.filter((appointment) => appointment.id !== id));
    await deleteAppointment(id);
  };

  const handleInputChange = (key, value) => {
    setNewAppointment((prev) => ({
      ...prev,
      [key]: value,
    }));
  };

  const handleServices = (id) => {
    setNewAppointment((previous) => ({
        ...previous,
        serviceIds: previous.serviceIds.includes(id) ? previous.serviceIds.filter((serviceId) => serviceId !== id) : [...previous.serviceIds, id],
    }))
  }

  const handleSubmit = async () => {
    await createAppointment(newAppointment)

    fetchData();
    setNewAppointment({ stylistId: '', customerId: '', time: '', serviceIds: [] });
  };


  return (
    //get all appointments
    <div>
      <h1> Create a new Appointment</h1>
      <label> Stylist:</label>
      <select
        value={newAppointment.stylistId}
        onChange={(event) =>
          handleInputChange("stylistId", parseInt(event.target.value))
        }
      >
        <option value=""> Select a stylist </option>
        {stylists.map((s) => (
          <option key={s.id} value={s.id}>
            {s.name}
          </option>
        ))}
      </select>
      <label> Customer:</label>
      <select
        value={newAppointment.customerId}
        onChange={(event) =>
          handleInputChange("customerId", parseInt(event.target.value))
        }
      >
        <option value=""> Select a customer </option>
        {customers.map((c) => (
          <option key={c.id} value={c.id}>
            {c.name}
          </option>
        ))}
      </select>
      <label> Time:</label>
      <input
        type="datetime-local"
        value={newAppointment.time}
        onChange={(event) => handleInputChange("time", event.target.value)}
      ></input>

      <label>Select Services: </label>
      {services.map((service) => (
        <div key={service.id}>
          <input
            type="checkbox"
            value={service.id}
            checked={newAppointment.serviceIds.includes(service.id)}
            onChange={() => handleServices(service.id)}
          ></input>
          {service.name}(${service.price})
        </div>
      ))}

      <button onClick={handleSubmit}>Create Appointment</button>

      <h1> Appointments </h1>
      <ul>
        {appointments.map((a) => (
          <li key={a.id}>
            {a.customer?.name} has an appointment at {a.time} with{" "}
            {a.stylist?.name} for a {a.services.map((s) => s.name).join(", ")}.
            The total cost will be ${a.totalCost}.{"  "}
            <button> Edit</button>
            <button onClick={() => handleDelete(a.id)}> Delete</button>
          </li> // delete appointments
        ))}
      </ul>
    </div>
  );
}

//create appointments
