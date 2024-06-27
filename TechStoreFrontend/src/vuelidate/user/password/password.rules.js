import { minLength, required } from "@vuelidate/validators";

const passwordRules = {
  currentPassword: { required, minLength: minLength(8) },
  newPassword: { required, minLength: minLength(8) },
};

export { passwordRules };
