<script setup>
import { ref } from "vue";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import FormContainer from "@/components/common/FormContainer.vue";
import { ITEM_CREATE_FAIL, ITEM_CREATE_SUCCESS } from "../../../constants/messages/create";

const emit = defineEmits(["reload"]);
const toast = useToast();
const code = ref("");
const discount = ref(0);
const loading = ref(false);

async function handleSave() {
  // TODO: add form validation and state
  // TODO: code and slug can be max 12 characters long
  // TODO: add discount validation (eg. check => 0 && <= 100)
  await axiosPrivate
    .post("/promo-codes", {
      code: code.value,
      discount: discount.value,
    })
    .then((resp) => {
      if (resp.status == 200) {
        code.value = "";
        discount.value = 0;
        toast.success(ITEM_CREATE_SUCCESS);
        emit("reload");
      }
    })
    .catch((error) => {
      console.log(error);
      toast.error(ITEM_CREATE_FAIL);
    });
}
</script>

<template>
  <form-container title="New Promo Code">
    <v-form @submit.prevent>
      <v-text-field
        v-model="code"
        class="mt-5"
        label="Code"
        density="compact"
        variant="outlined"
        hide-details="auto"
      />
      <v-text-field
        v-model="discount"
        type="number"
        class="mt-5"
        label="Discount"
        density="compact"
        variant="outlined"
        hide-details="auto"
        min="0"
        max="100"
      />
      <v-btn type="submit" class="form__btn" :loading="loading" @click="handleSave">Save</v-btn>
    </v-form>
  </form-container>
</template>

<style scoped>
.form__btn {
  margin-top: 30px;
  font-weight: bold;
  background: var(--ts-c-bg-light);
  color: var(--ts-c-text-dark);
  width: 100%;
  max-width: 300px !important;
}
</style>
