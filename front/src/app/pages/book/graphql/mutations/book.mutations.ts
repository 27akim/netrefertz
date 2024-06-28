import gql from 'graphql-tag';

export const CREATE_BOOK = gql`
  mutation CreateBook($command: CreateBookCommandInput!) {
    createBook(command: $command)
  }
`;

export const UPDATE_BOOK = gql`
  mutation updateBook($command: UpdateBookCommandInput!) {
    updateBook(
      command: $command
    ) {
      author
      id
      isbn
      publishedDate
      title
    }
  }
`;

export const DELETE_BOOK = gql`
  mutation DeleteBook($id: String!) {
    deleteBook(id: $id)
  }
`;
