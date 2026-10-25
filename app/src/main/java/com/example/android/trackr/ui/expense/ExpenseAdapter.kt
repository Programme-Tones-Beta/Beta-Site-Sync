/*
 * Copyright (C) 2023 The Android Open Source Project
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package com.example.android.trackr.ui.expense

import android.view.LayoutInflater
import android.view.ViewGroup
import androidx.recyclerview.widget.DiffUtil
import androidx.recyclerview.widget.ListAdapter
import androidx.recyclerview.widget.RecyclerView
import com.example.android.trackr.data.ExpenseItem
import com.example.android.trackr.databinding.ExpenseItemBinding

class ExpenseAdapter(
    private val onUploadClick: (ExpenseItem) -> Unit
) : ListAdapter<ExpenseItem, ExpenseAdapter.ViewHolder>(DiffCallback) {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val binding = ExpenseItemBinding.inflate(LayoutInflater.from(parent.context), parent, false)
        return ViewHolder(binding, onUploadClick)
    }

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        holder.bind(getItem(position))
    }

    class ViewHolder(
        private val binding: ExpenseItemBinding,
        private val onUploadClick: (ExpenseItem) -> Unit
    ) : RecyclerView.ViewHolder(binding.root) {
        fun bind(item: ExpenseItem) {
            binding.item = item
            binding.uploadButton.setOnClickListener { onUploadClick(item) }
            binding.executePendingBindings()
        }
    }

    companion object DiffCallback : DiffUtil.ItemCallback<ExpenseItem>() {
        override fun areItemsTheSame(oldItem: ExpenseItem, newItem: ExpenseItem): Boolean = oldItem.id == newItem.id
        override fun areContentsTheSame(oldItem: ExpenseItem, newItem: ExpenseItem): Boolean = oldItem == newItem
    }
}
