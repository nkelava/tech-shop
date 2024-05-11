<script setup>
import { reactive, computed, onMounted } from "vue";
import { useToast } from "vue-toastification";
import { useVuelidate } from "@vuelidate/core";
import { alpha, email, required, sameAs } from "@vuelidate/validators";
import { axiosPrivate } from "@/api/axios";
import BaseInput from "@/components/common/BaseInput.vue";
import HiddenInput from "@/components/common/BaseInputHidden.vue";

const toast = useToast();

const initialState = {
  firstName: "",
  lastName: "",
  phoneNumber: "",
};

const initialEmailState = {
  email: "",
};

const emailState = reactive({
  ...initialEmailState,
});

const emailRules = {
  email: { required, email },
};

const initialPasswordState = {
  currentPassword: "",
  newPassword: "",
  confirmPassword: "",
};

const passwordState = reactive({
  ...initialPasswordState,
});

const passwordRules = {
  currentPassword: { required },
  newPassword: { required },
  confirmPassword: { required, sameAs: sameAs(computed(() => passwordState.newPassword)) },
};

const state = reactive({
  ...initialState,
});

const rules = {
  firstName: { required, alpha },
  lastName: { required, alpha },
};

const v$ = useVuelidate(rules, state);
const ve$ = useVuelidate(emailRules, emailState);
const vp$ = useVuelidate(passwordRules, passwordState);

onMounted(async () => {
  const userInfo = await axiosPrivate.get("/users").catch((error) => console.log(error));

  if (userInfo?.data) {
    state.firstName = userInfo.data.firstName;
    state.lastName = userInfo.data.lastName;
    state.phoneNumber = userInfo?.data?.phoneNumber ?? "";
    emailState.email = userInfo.data.email;
  }
});

async function updateProfileInfo() {
  const isFormValid = await v$.value.$validate();

  if (!isFormValid) return;

  await axiosPrivate
    .post("/users/edit/profile", { ...state })
    .then(() => toast.success("Profile updated successfully."))
    .catch(() => toast.error("Failed to update your profile information. Please try again later."));
}

async function updateEmail() {
  const isFormValid = await ve$.value.$validate();

  if (!isFormValid) return;

  await axiosPrivate
    .post("/users/edit/email", { newEmail: emailState.email })
    .then(() => toast.success("Email updated successfully."))
    .catch(() => toast.error("Failed to update your email address. Please try again later."));
}

async function updatePassword() {
  const isFormValid = await vp$.value.$validate();

  if (!isFormValid) return;

  await axiosPrivate
    .post("/users/edit/password", {
      currentPassword: passwordState.currentPassword,
      newPassword: passwordState.newPassword,
    })
    .then(() => {
      toast.success("Password updated successfully.");
      clearForm(vp$, initialPasswordState, passwordState);
    })
    .catch(() =>
      toast.error(
        "Failed to update your password. Please ensure your new password meets the requirements and try again later."
      )
    );
}

const clearForm = (form, initialFormState, formState) => {
  form.value.$reset();

  for (const [key, value] of Object.entries(initialFormState)) {
    formState[key] = value;
  }
};
</script>

<template>
  <v-container class="account-details pa-10 rounded-lg">
    <v-row>
      <h2>My Details</h2>
    </v-row>
    <v-row class="details-item">
      <h3 class="details-item__title">Personal Information</h3>
      <v-divider color="warning"></v-divider>
      <v-col class="details-item__desc" cols="12">
        Lorem ipsum, dolor sit amet consectetur adipisicing elit. Laudantium nemo corporis fugiat.
        Quasi, similique ipsum.
      </v-col>
      <v-col cols="12">
        <form class="details-item__form">
          <v-row>
            <v-col class="pb-0" cols="12" lg="6">
              <base-input v-model="state.firstName" label="First Name" :v$="v$" />
            </v-col>
            <v-col class="pb-0" cols="12" lg="6">
              <base-input v-model="state.lastName" label="Last Name" :v$="v$" />
            </v-col>
            <v-col class="pt-0" cols="12">
              <base-input v-model="state.phoneNumber" label="Phone Number" />
              <v-btn class="details-item__btn" @click="updateProfileInfo"> Save </v-btn>
            </v-col>
          </v-row>
        </form>
      </v-col>
    </v-row>
    <v-row class="details-item">
      <h3 class="details-item__title">E-mail Address</h3>
      <v-divider color="warning"></v-divider>
      <v-col class="details-item__desc" cols="12">
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Ab ipsum beatae inventore, suscipit
        sequi rerum aperiam, dicta commodi velit fugit perferendis delectus odio consequatur et!
        Vitae ipsam adipisci animi iure.
      </v-col>
      <v-col cols="12">
        <form class="details-item__form">
          <base-input v-model="emailState.email" label="E-mail Address" :v$="ve$.email" />
          <v-btn class="details-item__btn" @click="updateEmail"> Save </v-btn>
        </form>
      </v-col>
    </v-row>
    <v-row class="details-item">
      <h3 class="details-item__title">Password</h3>
      <v-divider color="warning"></v-divider>
      <v-col class="details-item__desc" cols="12">
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Ab ipsum beatae inventore, suscipit
        sequi rerum aperiam, dicta commodi velit fugit perferendis delectus odio consequatur et!
        Vitae ipsam adipisci animi iure.
      </v-col>
      <v-col>
        <form class="details-item__form">
          <hidden-input
            v-model="passwordState.currentPassword"
            label="Current Password"
            :v$="vp$.currentPassword"
          />
          <hidden-input
            v-model="passwordState.newPassword"
            label="New Password"
            :v$="vp$.newPassword"
          />
          <hidden-input
            v-model="passwordState.confirmPassword"
            label="Confirm Password"
            :v$="vp$.confirmPassword"
          />
          <v-btn class="details-item__btn" @click="updatePassword"> Save </v-btn>
        </form>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>
.account-details {
  background-color: var(--ts-c-primary-soft);
  max-width: 1200px;
}

.details-item {
  margin-bottom: 20px;
}

.details-item__title {
  padding-bottom: 5px;
}

.details-item__title:first-child {
  margin-top: 30px;
}

.details-item__desc {
  opacity: 0.8;
}

.details-item__form {
  margin-top: 10px;
}

.details-item__btn {
  width: 100%;
  max-width: 250px;
  font-weight: bold;
  text-transform: capitalize;
  color: var(--ts-c-text-dark);
  background-color: var(--ts-c-ternary);
}

:deep(.v-divider) {
  margin-bottom: 1rem;
}
</style>
