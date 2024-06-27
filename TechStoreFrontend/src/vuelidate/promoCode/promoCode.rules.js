import { alphaNum, maxLength, maxValue, minValue, numeric, required } from "@vuelidate/validators";
import { helpers } from "@vuelidate/validators";

const futureDate = helpers.withMessage("The date must be in the future.", (value) => {
  if (!value) return false;

  const today = new Date();
  today.setHours(0, 0, 0, 0);
  const date = new Date(value);

  return date > today;
});

const promoCodeRules = {
  code: { required, alphaNum, maxLength: maxLength(12) },
  discount: { required, numeric, minValue: minValue(1), maxValue: maxValue(100) },
  expirationDate: { required, futureDate },
};

export { promoCodeRules };
