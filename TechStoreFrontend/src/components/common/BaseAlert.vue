<script setup>
import { ref } from "vue";

const props = defineProps(["title", "message"]);
const emit = defineEmits(["toggleShowAlert"]);
const isActive = ref(true);

const close = () => {
  isActive.value = !isActive.value;
  emit("toggleShowAlert");
};
</script>
<template>
  <v-alert
    class="alert"
    v-model="isActive"
    border="start"
    close-label="Close Alert"
    :title="props.title"
    @click:close="close"
    closable
  >
    {{ props.message }}
  </v-alert>
</template>

<style scoped>
.alert {
  position: fixed;
  bottom: 1rem;
  left: 50%;
  transform: translate(-50%, -50%);
}
</style>
