import { useQuery, UseQueryResult } from "@tanstack/react-query";
import News from "../News";

export default function useGetNewsQuery() : UseQueryResult<void, Error> {
  return useQuery({
    queryKey: ['api/News'],
    queryFn: () => {
        return News.getAllNews()
    }
  })
}
