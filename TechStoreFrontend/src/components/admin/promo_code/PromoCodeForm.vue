<script setup>
import { ref, watch, computed } from "vue";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import FormContainer from "@/components/common/FormContainer.vue";
import { ITEM_CREATE_FAIL, ITEM_CREATE_SUCCESS } from "../../../constants/messages/create";
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
const code = ref("");
const discount = ref(0);
const expirationDate = ref(getTomorrowDate());
const loading = ref(false);

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
      code.value = data.code;
      discount.value = data.discount;
      expirationDate.value = new Date(data.expirationDate).toISOString().substring(0, 10);
    }
  } catch (error) {
    console.error(error);
    toast.error("Failed to load promo code.");
  }
}

function resetForm() {
  props.id = null;
  code.value = "";
  discount.value = 0;
  expirationDate.value = getTomorrowDate();
}

const handleSave = async () => {
  loading.value = true;

  if (code.value.length > 12) {
    toast.error("Code must be less than 12 characters.");
    loading.value = false;
    return;
  }

  if (discount.value < 0 || discount.value > 100) {
    toast.error("Discount must be between 0 and 100.");
    loading.value = false;
    return;
  }

  const payload = {
    code: code.value,
    discount: discount.value,
    expirationDate: expirationDate.value,
  };

  try {
    let resp;

    if (props.id) {
      resp = await axiosPrivate.put(`/promo-codes/${props.id}`, payload);
    } else {
      resp = await axiosPrivate.post("/promo-codes", payload);
    }

    if (resp.status === 200) {
      resetForm();
      toast.success(ITEM_CREATE_SUCCESS);
      emit("clearSelectedId");
      emit("reload");
    }
  } catch (error) {
    console.error(error);
    toast.error(ITEM_CREATE_FAIL);
  } finally {
    loading.value = false;
  }
};

const handleCancel = () => {
  resetForm();
  emit("clearSelectedId");
};
</script>

<template>
  <form-container :title="formTitle">
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
        :min="0"
        :max="100"
      />
      <v-text-field
        v-model="expirationDate"
        type="date"
        class="mt-5"
        label="Expiration Date"
        density="compact"
        variant="outlined"
        hide-details="auto"
        :min="getTomorrowDate()"
      ></v-text-field>
      <v-btn
        v-if="props?.id"
        type="submit"
        class="form__btn mr-5"
        :loading="loading"
        :disabled="loading"
        @click="handleCancel"
        >Cancel</v-btn
      >
      <v-btn
        type="submit"
        class="form__btn"
        :loading="loading"
        :disabled="loading"
        @click="handleSave"
        >Save</v-btn
      >
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
