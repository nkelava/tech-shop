<script setup>
import { ref, watch, computed, reactive } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import BaseInput from "@/components/common/BaseInput.vue";
import FormContainer from "@/components/common/FormContainer.vue";
import { initialPromoCodeState, promoCodeRules } from "@/vuelidate/promoCode";
import { ITEM_CREATE_FAIL, ITEM_CREATE_SUCCESS } from "@/constants/messages/create";
import { ITEM_UPDATE_FAIL, ITEM_UPDATE_SUCCESS } from "@/constants/messages/update";
import { getTomorrowDate } from "@/helpers/getTomorrowDate";

const props = defineProps({
  id: {
    type: [String, Number],
    required: false,
    default: null,
  },
});
const emit = defineEmits(["reload", "clearSelectedId"]);
const toast = useToast();
const promoCodeState = reactive({ ...initialPromoCodeState });
const loading = ref(false);

const v$ = useVuelidate(promoCodeRules, promoCodeState);

watch(
  () => props.id,
  (newId) => {
    if (newId) {
      loadPromoCode(newId);
    } else {
      resetForm();
    }
  },
  { immediate: true }
);

const formTitle = computed(() => (props.id ? "Edit Promo Code" : "New Promo Code"));

async function loadPromoCode(id) {
  try {
    const { data, status } = await axiosPrivate.get(`/promo-codes/${id}`);

    if (status === 200) {
      promoCodeState.code = data?.code;
      promoCodeState.discount = data?.discount;
      promoCodeState.expirationDate = new Date(data?.expirationDate).toISOString().substring(0, 10);
    }
  } catch (error) {
    toast.error("Failed to load promo code.");
    console.error(error);
  }
}

const handleSave = async () => {
  if (!(await v$.value.$validate())) return;

  loading.value = true;

  const payload = {
    code: promoCodeState?.code,
    discount: promoCodeState?.discount,
    expirationDate: promoCodeState?.expirationDate,
  };

  try {
    let resp;

    if (props.id) {
      resp = await axiosPrivate.put(`/promo-codes/${props.id}`, payload);
    } else {
      resp = await axiosPrivate.post("/promo-codes", payload);
    }

    if (resp?.status === 200) {
      resetForm();
      toast.success(props?.id ? ITEM_UPDATE_SUCCESS : ITEM_CREATE_SUCCESS);
      emit("clearSelectedId");
      emit("reload");
    }
  } catch (error) {
    toast.error(
      error?.response?.data ? error.response.data : props?.id ? ITEM_UPDATE_FAIL : ITEM_CREATE_FAIL
    );
    console.error(error);
  } finally {
    loading.value = false;
  }
};

const handleCancel = () => {
  resetForm();
  emit("clearSelectedId");
};

function resetForm() {
  props.id = null;
  v$.value.$reset();
  Object.assign(promoCodeState, initialPromoCodeState);
}
</script>

<template>
  <form-container :title="formTitle">
    <v-form @submit.prevent="handleSave">
      <base-input
        v-model="promoCodeState.code"
        class="mt-5"
        name="code"
        label="Code"
        :v$="v$.code"
        density="compact"
        hide-details="auto"
      />
      <base-input
        v-model="promoCodeState.discount"
        type="number"
        class="mt-5"
        name="discount"
        label="Discount"
        :v$="v$.discount"
        density="compact"
        variant="outlined"
        hide-details="auto"
        :min="0"
        :max="100"
      />
      <base-input
        v-model="promoCodeState.expirationDate"
        type="date"
        class="mt-5"
        name="expirationDate"
        label="Expiration Date"
        :v$="v$.expirationDate"
        density="compact"
        variant="outlined"
        hide-details="auto"
        :min="getTomorrowDate()"
      />
      <v-btn
        v-if="props?.id"
        type="button"
        class="form__btn mr-5"
        :loading="loading"
        :disabled="loading"
        @click="handleCancel"
        >Cancel</v-btn
      >
      <v-btn type="submit" class="form__btn" :loading="loading" :disabled="loading">Save</v-btn>
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
