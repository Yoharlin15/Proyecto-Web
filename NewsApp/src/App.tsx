import './App.css';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { ReactQueryDevtools } from '@tanstack/react-query-devtools'
import NewsView from './Views/News';

function App() : JSX.Element{   

  return (
   <QueryClientProvider client={ new QueryClient()}>
   <NewsView/>
   <ReactQueryDevtools initialIsOpen={false}/> 
   </QueryClientProvider>
  )
}

export default App
