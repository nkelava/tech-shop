<script setup>
import { reactive, computed, onMounted } from "vue";
import { useRouter } from "vue-router";
import { useToast } from "vue-toastification";
import { useVuelidate } from "@vuelidate/core";
import { minLength, required, sameAs } from "@vuelidate/validators";
import { axiosPrivate } from "@/api/axios";
import { useUserStore } from "@/store";
import {
  emailRules,
  initialEmailState,
  infoRules,
  initialInfoState,
  passwordRules,
  initialPasswordState,
} from "@/vuelidate/user";
import BaseInput from "@/components/common/BaseInput.vue";
import HiddenInput from "@/components/common/BaseInputHidden.vue";
import { LOGOUT_SUCCESS, LOGOUT_FAIL } from "@/constants/messages/auth.js";

const router = useRouter();
const toast = useToast();
const userStore = useUserStore();
const infoState = reactive({ ...initialInfoState });
const passwordState = reactive({ ...initialPasswordState });
const emailState = reactive({ ...initialEmailState });

const passwordValidationRules = computed(() => {
  passwordRules.confirmPassword = {
    required,
    sameAs: sameAs(computed(() => passwordState.newPassword)),
    minLength: minLength(8),
  };
  return passwordRules;
});

const v$ = useVuelidate(infoRules, infoState);
const ve$ = useVuelidate(emailRules, emailState);
const vp$ = useVuelidate(passwordValidationRules, passwordState);

onMounted(async () => {
  try {
    const userInfo = await axiosPrivate.get("/users");

    if (userInfo?.data) {
      infoState.firstName = userInfo.data.firstName;
      infoState.lastName = userInfo.data.lastName;
      infoState.phoneNumber = userInfo?.data?.phoneNumber ?? "";
      emailState.email = userInfo.data.email;
    }
  } catch (error) {
    console.error("Error fetching user information:", error);
    toast.error("Failed to load user information. Please try again later.");
  }
});

const updateProfileInfo = async () => {
  const isFormValid = await v$.value.$validate();

  if (!isFormValid) return;

  try {
    await axiosPrivate.post("/users/edit/profile", { ...infoState });
    toast.success("Profile updated successfully.");
  } catch (error) {
    toast.error("Failed to update your profile information. Please try again later.");
  }
};

const updateEmail = async () => {
  const isFormValid = await ve$.value.$validate();

  if (!isFormValid) return;

  try {
    await axiosPrivate.post("/users/edit/email", { newEmail: emailState.email });
    toast.success("Email updated successfully.");
    handleLogout();
  } catch (error) {
    toast.error("Failed to update your email address. Please try again later.");
  }
};

async function handleLogout() {
  try {
    await userStore.logoutUser();

    toast.success(LOGOUT_SUCCESS);
    router.push("/auth");
  } catch (error) {
    if (error.response) {
      console.log(error.response);
    } else {
      console.log(`Error: ${error.message}`);
    }
    toast.error(LOGOUT_FAIL);
  }
}

const updatePassword = async () => {
  const isFormValid = await vp$.value.$validate();

  if (!isFormValid) return;

  try {
    await axiosPrivate.post("/users/edit/password", {
      currentPassword: passwordState.currentPassword,
      newPassword: passwordState.newPassword,
    });
    toast.success("Password updated successfully.");
    resetForm(vp$, initialPasswordState, passwordState);
  } catch (error) {
    toast.error(
      "Failed to update your password. Please ensure your new password meets the requirements and try again later."
    );
  }
};

const resetForm = (form, initialFormState, formState) => {
  form.value.$reset();
  Object.assign(formState, initialFormState);
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
        Manage your personal details here. Keep your name, email, phone number, and address
        up-to-date to ensure seamless service and communication. Your privacy is our priority.
      </v-col>
      <v-col cols="12">
        <form class="details-item__form">
          <v-row>
            <v-col class="pb-0" cols="12" lg="6">
              <base-input v-model="infoState.firstName" label="First Name" :v$="v$" />
            </v-col>
            <v-col class="pb-0" cols="12" lg="6">
              <base-input v-model="infoState.lastName" label="Last Name" :v$="v$" />
            </v-col>
            <v-col class="pt-0" cols="12">
              <base-input v-model="infoState.phoneNumber" label="Phone Number" />
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
        Update your email address here. Ensure it’s current to receive important account
        notifications and stay connected. Your email is securely stored and never shared.
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
        Change your password regularly to keep your account secure. Choose a strong password to
        protect your personal information and ensure the safety of your account.
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
