import { required, email, alpha } from "@vuelidate/validators";

const rules = {
  firstName: { required, alpha },
  lastName: { required, alpha },
  email: { required, email },
  address: { required },
  city: { required },
  zipCode: { required },
  country: { required },
  phone: { required },
};

export { rules };
