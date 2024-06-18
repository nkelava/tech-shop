<script setup>
import { computed, ref, watch } from "vue";
import { useToast } from "vue-toastification";
import { axiosPrivate } from "@/api/axios";
import FormContainer from "@/components/common/FormContainer.vue";
import { ITEM_CREATE_FAIL, ITEM_CREATE_SUCCESS } from "@/constants/messages/create";
import { ITEM_UPDATE_FAIL, ITEM_UPDATE_SUCCESS } from "@/constants/messages/update";

// TODO: Add valdator and init state
const props = defineProps({
  id: {
    type: [String, Number],
    required: false,
    default: null,
  },
});
const emit = defineEmits(["reload", "clearSelectedId"]);
const toast = useToast();
const name = ref("");
const loading = ref(false);

watch(
  () => props.id,
  (newId) => {
    if (newId) {
      loadAttribute(newId);
    } else {
      resetForm();
    }
  },
  { immediate: true }
);

const formTitle = computed(() => (props.id ? "Edit Attribute" : "New Attribute"));

async function loadAttribute(id) {
  try {
    const { data, status } = await axiosPrivate.get(`/attributes/${id}`);

    if (status === 200) {
      name.value = data.name;
    }
  } catch (error) {
    console.error(error);
    toast.error("Failed to load attribute.");
  }
}

function resetForm() {
  props.id = null;
  name.value = "";
}

async function handleSave() {
  loading.value = true;

  const payload = {
    name: name.value,
  };

  try {
    let resp;

    if (props?.id) {
      resp = await axiosPrivate.put(`/attributes/${props.id}`, payload);
    } else {
      resp = await axiosPrivate.post("/attributes", payload);
    }

    if (resp.status == 200) {
      resetForm();
      toast.success(props?.id ? ITEM_UPDATE_SUCCESS : ITEM_CREATE_SUCCESS);
      emit("clearSelectedId");
      emit("reload");
    }
  } catch (error) {
    console.log(error);
    toast.error(props?.id ? ITEM_UPDATE_FAIL : ITEM_CREATE_FAIL);
  } finally {
    loading.value = false;
  }
}

const handleCancel = () => {
  resetForm();
  emit("clearSelectedId");
};
</script>

<template>
  <form-container :title="formTitle">
    <v-form @submit.prevent>
      <v-text-field
        v-model="name"
        class="mt-5"
        label="Name"
        density="compact"
        variant="outlined"
        hide-details="auto"
      />
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
