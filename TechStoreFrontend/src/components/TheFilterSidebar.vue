<script setup>
import { ref, defineEmits } from "vue";
import { getSubcategoryAttributesWithValues } from "@/database/services/subcategoryService.js";

const emit = defineEmits(["filter"]);
const { subcategoryId } = defineProps(["subcategoryId"]);
const subcategoryAttributesWithValues = getSubcategoryAttributesWithValues(subcategoryId);
const sliderValue = ref(0);
const filterMap = ref({});

const updateFilters = (event, attribute, attributeValue) => {
  // Check if attribute does not exist in filter map
  if (!filterMap.value.hasOwnProperty(attribute)) {
    filterMap.value[attribute] = new Set([attributeValue]);
    emit("filter", filterMap.value);
    return;
  }

  // Attribute already exists in filter map - check if option is already checked or is it a new option
  if (filterMap.value[attribute].has(attributeValue)) {
    filterMap.value[attribute].delete(attributeValue);
    if (!filterMap.value[attribute].size) delete filterMap.value[attribute];
    event.target.checked = false;
    emit("filter", filterMap.value);
    return;
  }

  filterMap.value[attribute].add(attributeValue);
  emit("filter", filterMap.value);
};

const handleFilter = () => {
  emit("filter", filterMap.value);
};
</script>

<template>
  <div class="sidebar">
    <div class="item">
      <h3 class="item__title">Price</h3>
      <hr />
      <div class="price-container">
        <input type="number" placeholder="From..." value="0" />
        <input type="number" placeholder="To..." value="0" />
      </div>
    </div>
    <div class="item">
      <h3 class="item__title">Rating</h3>
      <hr />
      <!-- TODO: create slider component in case of keeping this filter -->
      <div class="slider">
        <input v-model="sliderValue" type="range" min="0" max="5" step="1" />
        <span>{{ sliderValue }}</span>
      </div>
    </div>
    <div v-for="attribute in subcategoryAttributesWithValues" :key="attribute.id" class="item">
      <h3 class="item__title">{{ attribute.name }}</h3>
      <hr />
      <div v-for="attributeValue in attribute.values" :key="attributeValue.id">
        <input
          type="checkbox"
          id="lorem"
          @click="updateFilters($event, attribute.name, attributeValue.value)"
        />
        <label for="lorem">{{ attributeValue.value }} {{ attributeValue.unit || "" }}</label>
      </div>
    </div>
    <button @click="handleFilter">Filter</button>
  </div>
</template>

<style scoped>
.sidebar {
  background-color: var(--ts-c-bg-light);
  border-radius: 10px;
  color: var(--ts-c-text-dark);
  display: flex;
  flex-direction: column;
}

.item {
  margin-inline: 20px;
  margin: 1rem 2rem;
}

.item__title {
  margin: 5px;
}

hr {
  margin-bottom: 1rem;
  margin-right: 2rem;
}

.slider {
  display: flex;
  align-items: center;
}

input[type="range"] {
  width: 185px;
  margin-right: 10px;
}

input[type="checkbox"] {
  margin-right: 10px;
}

button {
  height: 30px;
  width: 100%;
}
</style>
