import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import {
  editAppointment,
  getAppointmentById,
} from "../services/AppointmentServices";
import { getServices } from "../services/ServicesServices";

export default function EditAppointment() {
  const [appointment, setAppointment] = useState({
    serviceIds: [],
  });
  const [services, setServices] = useState([]);
  const { id } = useParams();

useEffect(() => {
  const fetchData = async () => {
    const fetchedAppointment = await getAppointmentById(id);
    const allServices = await getServices();

    // Create serviceIds from Services array
    const serviceIds = fetchedAppointment.services?.map((s) => s.id) || [];

    setAppointment({ ...fetchedAppointment, serviceIds });
    setServices(allServices);
  };

  fetchData();
}, [id]);


  const handleServices = (id) => {
    setAppointment((previous) => ({
      ...previous,
      serviceIds: previous.serviceIds.includes(id)
        ? previous.serviceIds.filter((serviceId) => serviceId !== id)
        : [...previous.serviceIds, id],
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      await editAppointment(id, appointment.serviceIds);
      alert("Appointment updated successfully!");
    } catch (error) {
      console.error(error);
      alert("There was an error updating the appointment.");
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Edit Appointment Services</h2>
      {services.map((service) => (
        <div key={service.id}>
          <input
            type="checkbox"
            value={service.id}
            checked={appointment.serviceIds?.includes(service.id)}
            onChange={() => handleServices(service.id)}
          ></input>
          {service.name}(${service.price})
        </div>
      ))}

      <button type="submit">Update Appointment</button>
    </form>
  );
}
