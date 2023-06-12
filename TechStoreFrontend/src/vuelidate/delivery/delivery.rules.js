import { required, alpha } from "@vuelidate/validators";

const rules = {
  firstName: { required, alpha },
  lastName: { required, alpha },
  address: { required },
  city: { required },
  zipCode: { required },
  country: { required },
};

export { rules };
