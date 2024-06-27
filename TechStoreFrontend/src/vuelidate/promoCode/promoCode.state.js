import { getTomorrowDate } from "@/helpers/getTomorrowDate";

const initialPromoCodeState = {
  code: "",
  discount: 0,
  expirationDate: getTomorrowDate(),
};

export { initialPromoCodeState };
