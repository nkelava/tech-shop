<script setup>
import { computed, reactive, ref } from "vue";
import { useRouter } from "vue-router";
import useVuelidate from "@vuelidate/core";
import { required, sameAs } from "@vuelidate/validators";
import { useToast } from "vue-toastification";
import { useUserStore } from "@/store";
import BaseInput from "@/components/common/BaseInput.vue";
import HiddenInput from "@/components/common/BaseInputHidden.vue";
import { initialState as initialLoginState, rules as loginRules } from "@/vuelidate/auth/login";
import {
  initialState as initialRegisterState,
  rules as registerRules,
} from "@/vuelidate/auth/register";
import {
  LOGIN_SUCCESS,
  LOGIN_FAIL,
  REGISTER_SUCCESS,
  REGISTER_FAIL,
  CONFIRMATION_FAIL,
} from "@/constants/messages/auth.js";

const router = useRouter();
const toast = useToast();
const userStore = useUserStore();
const isRightPanelActive = ref(false);
const loginState = reactive({ ...initialLoginState });
const registerState = reactive({ ...initialRegisterState });
const loginValidationRules = computed(() => loginRules);
const registerValidationRules = computed(() => {
  registerRules.confirmPassword = {
    required,
    sameAs: sameAs(computed(() => registerState.password)),
  };
  return registerRules;
});
const vl$ = useVuelidate(loginValidationRules, loginState);
const vr$ = useVuelidate(registerValidationRules, registerState);

const togglePanel = () => {
  isRightPanelActive.value = !isRightPanelActive.value;
};

const handleSignIn = async () => {
  if (!(await vl$.value.$validate())) return;

  const { email, password } = loginState;

  try {
    await userStore.loginUser(email, password);

    toast.success(LOGIN_SUCCESS);
    resetForm(vl$, initialLoginState, loginState);
    router.push("/");
  } catch (error) {
    console.log(error.response.status);
    const toastMsg = error.response.status === 403 ? CONFIRMATION_FAIL : LOGIN_FAIL;
    toast.error(toastMsg);
    console.log(`Error: ${error}`);
  }
};

const handleSignUp = async () => {
  if (!(await vr$.value.$validate())) return;

  const { firstName, lastName, email, password, confirmPassword } = registerState;

  try {
    await userStore.registerUser(firstName, lastName, email, password, confirmPassword);
    toast.success(REGISTER_SUCCESS);
    resetForm(vr$, initialRegisterState, registerState);
  } catch (error) {
    toast.error(REGISTER_FAIL);
    console.log(`Error: ${error}`);
  }
};

const resetForm = (form, initialFormState, formState) => {
  form.value.$reset();
  Object.assign(formState, initialFormState);
};
</script>

<template>
  <div class="container" :class="{ 'right-panel-active': isRightPanelActive }">
    <div class="form-container sign-up-container">
      <form @submit.prevent>
        <h1>Create Account</h1>
        <base-input
          class="w-100"
          v-model="registerState.firstName"
          label="First Name"
          :v$="vr$.firstName"
          density="compact"
        />
        <base-input
          class="w-100"
          v-model="registerState.lastName"
          label="Last Name"
          :v$="vr$.lastName"
          density="compact"
        />
        <base-input
          class="w-100"
          v-model="registerState.email"
          label="Email"
          :v$="vr$.email"
          density="compact"
        />
        <hidden-input
          class="w-100"
          v-model="registerState.password"
          label="Password"
          :v$="vr$.password"
          density="compact"
        />
        <hidden-input
          v-model="registerState.confirmPassword"
          class="w-100"
          label="Confirm Password"
          :v$="vr$.confirmPassword"
          density="compact"
        />
        <button @click="handleSignUp">Sign Up</button>
      </form>
    </div>
    <div class="form-container sign-in-container">
      <form @submit.prevent>
        <h1>Sign in</h1>
        <base-input
          class="w-100"
          v-model="loginState.email"
          label="Email"
          :v$="vl$.email"
          density="compact"
        />
        <hidden-input
          class="w-100"
          v-model="loginState.password"
          label="Password"
          :v$="vl$.password"
          density="compact"
        />
        <!-- <a href="#">Forgot your password?</a> -->
        <button @click="handleSignIn">Sign In</button>
      </form>
    </div>
    <div class="overlay-container">
      <div class="overlay">
        <div class="overlay-panel overlay-left">
          <h1>Welcome Back!</h1>
          <p>To keep connected with us please login with your personal information</p>
          <button class="ghost" id="signIn" @click="togglePanel">Sign In</button>
        </div>
        <div class="overlay-panel overlay-right">
          <h1>Hello, Friend!</h1>
          <p>Enter your personal details and start journey with us</p>
          <button class="ghost" id="signUp" @click="togglePanel">Sign Up</button>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.overlay-panel p {
  margin: 20px 0 30px;
  letter-spacing: 0.5px;
  line-height: 20px;
}

button {
  padding: 12px 45px;
  font-weight: bold;
  color: #ffffff;
  background-color: var(--ts-c-bg-dark);
  border-radius: 10px;
  border: 1px solid var(--ts-c-primary-dark);
  transition: transform 80ms ease-in;
}

button:active {
  transform: scale(0.95);
}

button.ghost {
  background-color: transparent;
  border-color: #ffffff;
}

form {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  gap: 0.5rem;
  padding: 0 50px;
  color: var(--ts-c-text-dark);
  background-color: var(--ts-c-bg-light);
}

input {
  width: 100%;
  padding: 12px 15px;
  margin: 8px 0;
  background-color: #eee;
  border: none;
  border-radius: 10px;
}

.container {
  position: relative;
  overflow: hidden;
  display: flex !important;
  flex-direction: column !important;
  width: 900px;
  max-width: 100%;
  min-height: 580px;
  margin: 5rem auto;
  background-color: var(--ts-c-bg-light);
  border-radius: 10px;
  box-shadow: 0 14px 28px rgba(0, 0, 0, 0.25), 0 10px 10px rgba(0, 0, 0, 0.22);
}

.form-container {
  position: absolute;
  display: flex;
  flex-direction: column;
  justify-content: center;
  top: 0;
  height: 100%;
  transition: all 0.6s ease-in-out;
}

.sign-in-container {
  z-index: 2;
  left: 0;
  width: 50%;
}

.container.right-panel-active .sign-in-container {
  transform: translateX(100%);
}

.sign-up-container {
  left: 0;
  width: 50%;
  opacity: 0;
  z-index: 1;
}

.container.right-panel-active .sign-up-container {
  z-index: 5;
  opacity: 1;
  transform: translateX(100%);
  animation: show 0.6s;
}

@keyframes show {
  0%,
  49.99% {
    opacity: 0;
    z-index: 1;
  }

  50%,
  100% {
    opacity: 1;
    z-index: 5;
  }
}

.overlay-container {
  position: absolute;
  z-index: 100;
  top: 0;
  left: 50%;
  width: 50%;
  height: 100%;
  overflow: hidden;
  transition: transform 0.6s ease-in-out;
}

.container.right-panel-active .overlay-container {
  transform: translateX(-100%);
}

.overlay {
  position: relative;
  left: -100%;
  width: 200%;
  height: 100%;
  color: #fff;
  background: var(--ts-c-primary-soft);
  background-repeat: no-repeat;
  background-size: cover;
  background-position: 0 0;
  transform: translateX(0);
  transition: transform 0.6s ease-in-out;
}

.container.right-panel-active .overlay {
  transform: translateX(50%);
}

.overlay-panel {
  position: absolute;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  top: 0;
  height: 100%;
  width: 50%;
  padding: 0 40px;
  text-align: center;
  transform: translateX(0);
  transition: transform 0.6s ease-in-out;
}

.overlay-left {
  transform: translateX(-20%);
}

.container.right-panel-active .overlay-left {
  transform: translateX(0);
}

.overlay-right {
  right: 0;
  transform: translateX(0);
}

.container.right-panel-active .overlay-right {
  transform: translateX(20%);
}
</style>
