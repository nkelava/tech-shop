export const formatDate = (dateToFormat) => {
  const date = new Date(dateToFormat);
  const formattedDate = `${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()}`;

  return formattedDate;
};
