import { Table } from "antd";
import useGetNewsQuery from "../../hooks/use-get-new-query";

export default function NewsView(): JSX.Element {
  const newsQuery = useGetNewsQuery();

  return (
    <div>
      <Table
        loading={newsQuery.isLoading}
        dataSource={newsQuery.data ?? []}
        columns={[
          {
            title: "Title",
            dataIndex: "Title",
            key: "Title"
          },
          {
            title: "Author",
            dataIndex: "Author",
            key: "Author"
          },
          {
            title: "Description",
            dataIndex: "Description",
            key: "Description"
          },
          {
            title: "Published Date",
            dataIndex: "PublishedDate",
            key: "PublishedDate"
          }
        ]}
      />
    </div>
  );
}



