import { AxiosResponse } from "axios";
import client from "../Client";

export async function getAllNews(): Promise<{ id: number; name: string }[]> {

  const response: AxiosResponse<any, any> = await client.get("/api/News");

    return response.data as 
    { 
    id: number; 
    name: string 
    }[];
}

export default {
    getAllNews
}

 