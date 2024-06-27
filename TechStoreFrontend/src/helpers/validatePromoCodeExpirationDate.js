export const validatePromoCodeExpirationDate = (promoCode) => {
  const currentDate = new Date();
  let expDate = new Date(promoCode?.expirationDate);

  if (currentDate > expDate) {
    return false;
  } else {
    return true;
  }
};
