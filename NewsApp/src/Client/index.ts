import axios  from "axios";

const client = axios.create({
  baseURL: "http://localhost:3000", // Reemplaza esto con la URL de tu API
});

export default client
