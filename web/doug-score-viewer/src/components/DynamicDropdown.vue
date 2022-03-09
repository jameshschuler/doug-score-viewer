<template>
  <div class="field">
    <label class="label">{{ props.label }}</label>
    <div class="control">
      <div class="select is-fullwidth" :class="{ 'is-loading': loading, 'is-danger': error }">
        <select :disabled="props.disabled" @change="$emit('update:selectedValue', ($event.target! as HTMLSelectElement).value)">
          <option value="">Select {{ props.label }}</option>
          <option v-for="option in data" v-bind:value="option.value">
            {{ option.text }}
          </option>
        </select>
      </div>
    </div>
    <p v-if="error" class="help is-danger">{{ error }}</p>
  </div>
</template>
<script setup lang="ts">
import { ref } from "vue";
import { APIResponse, Option } from "../models/common";
import { OptionsResponse } from "../models/response";

const props = defineProps({
  dataEndpoint: Function,
  label: String,
  disabled: Boolean,
  selectedValue: String,
});

const data = ref<Option[]>([]);
const error = ref<string>();
const loading = ref<boolean>();

async function loadOptions() {
  if (props.dataEndpoint && !props.disabled) {
    loading.value = true;

    const response = (await props.dataEndpoint!()) as APIResponse<OptionsResponse>;

    if (response.error) {
      error.value = response.error.message;
    } else {
      data.value = response.data?.options ?? [];
    }

    loading.value = false;
  }
}

loadOptions();
</script>
<style lang="scss"></style>
