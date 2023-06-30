import { promoCodesDb } from "../seed/promo-codes.js";

export function getPromoCodes() {
  return promoCodesDb;
}

export function getPromoCode(code) {
  return promoCodesDb.find((promoCode) => promoCode.code.toLowerCase() === code.toLowerCase());
}

export function getPromoCodeDiscount(code) {
  const promoCode = promoCodesDb.find(
    (promoCode) => promoCode.code.toLowerCase() === code.toLowerCase()
  );
  return promoCode.discount;
}
